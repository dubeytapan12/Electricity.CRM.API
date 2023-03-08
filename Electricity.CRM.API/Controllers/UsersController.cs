using AutoMapper;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Electricity.CRM.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [Route("refresh")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = _jWTManager.GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = _userServiceRepository.GetSavedRefreshTokens(username, token.Refresh_Token);

            if ((savedRefreshToken==null) || (savedRefreshToken.RefreshToken != token.Refresh_Token))
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
            _userServiceRepository.AddUserRefreshTokens(obj);
            _userServiceRepository.SaveCommit();

            return Ok(newJwtToken);
        }
    }
}
