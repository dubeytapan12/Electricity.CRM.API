using AutoMapper;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Electricity.CRM.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IManageJWTRepository _jWTManager;
        private readonly IUserServiceRepository _userServiceRepository;
        private readonly IMapper _mapper;

        public UsersController(IManageJWTRepository jWTManager, IUserServiceRepository userServiceRepository, IMapper mapper)
        {
            _jWTManager = jWTManager;
            _userServiceRepository = userServiceRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(UserDto user)
        {
            var userdata = _mapper.Map<User>(user);
            var validUser = await _userServiceRepository.IsValidUserAsync(userdata);

            if (!validUser)
            {
                return Unauthorized("Incorrect username or password!");
            }

            var token = _jWTManager.GenerateToken(userdata.UserName);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = token.Refresh_Token,
                UserName = userdata.UserName
            };

            _userServiceRepository.AddUserRefreshTokens(obj);
            _userServiceRepository.SaveCommit();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reset-password")]
        public IActionResult ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            _userServiceRepository.UpdateUserPassword(resetPassword.UserName,resetPassword.Token, resetPassword.Password);
            _userServiceRepository.SaveCommit();
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("forgot-password/{userName}")]
        public async Task<IActionResult> ForgotPasswordAsync(string userName)
        {
            var user = await _userServiceRepository.GetUserByUserName(userName);
            if (user == null)
            {
                throw new System.Exception("Invalid user!");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new System.Exception("email not found for user!");
            }
            var key = GetForgotToken();
            key = key.Replace("=", "");
            key = key.Replace("/", "");
            key = key.Replace("\"", "");
            string html = "<html><body><h1>Electricity CRM: Forgot Password</h1><a href=\"http://localhost:4200/forgot-password/" + user.UserName + "/" + key + "\">Please click On link to reset</a></body></html>";
            _userServiceRepository.UpdateUserToken(user.UserName, key);
            _userServiceRepository.SaveCommit();
            EmailService.Email(html, "Forgot-password", user.Email);
            return Ok(html);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("valid-user/{userName}/{token}")]
        public async Task<IActionResult> ValidUser(string userName, string token)
        {
            var isValid = await _userServiceRepository.IsValidUserForReset(userName, token);
            return Ok(isValid);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = _jWTManager.GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = _userServiceRepository.GetSavedRefreshTokens(username, token.Refresh_Token);

            if ((savedRefreshToken == null) || (savedRefreshToken.RefreshToken != token.Refresh_Token))
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = _jWTManager.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = newJwtToken.Refresh_Token,
                UserName = username
            };

            _userServiceRepository.DeleteUserRefreshTokens(username, token.Refresh_Token);
            _userServiceRepository.DeleteAllUserTokens(username);
            _userServiceRepository.AddUserRefreshTokens(obj);
            _userServiceRepository.SaveCommit();

            return Ok(newJwtToken);
        }

        private string GetForgotToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
