using AutoMapper;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Factory;
using Electricity.CRM.API.UnityOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Electricity.CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ElectricityUserController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public ElectricityUserController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        // GET: api/<ElectricityUserController>
        [HttpGet]
        [Route("all-users")]
        public async Task<IEnumerable<ElectricityUserDtos>> GetAllUsers()
        {
            List<ElectricityUserDtos> lst = new List<ElectricityUserDtos>();
            lst.AddRange(await _unityOfWork.ElectricityCommercialRepository.GetElectricityUsers());
            lst.AddRange(await _unityOfWork.ElectricityFactoryRepository.GetElectricityUsers());
            lst.AddRange(await _unityOfWork.ElectricityFlatRepository.GetElectricityUsers());
            lst.AddRange(await _unityOfWork.ElectricityResidentialRepository.GetElectricityUsers());
            return lst;
        }
        [HttpGet]
        [Route("users/connectionType/{connectionType}")]
        public async Task<IEnumerable<ElectricityUserDtos>> GetUsers(string connectionType)
        {
            var item = ElectricityUserFactory.GetElectricityUserFactory(connectionType, _unityOfWork);
            return await item.GetElectricityUsers();
        }

        // GET api/<ElectricityUserController>/5
        [HttpGet]
        [Route("users/connectionType/{connectionType}/id/{id}")]
        public async Task<ElectricityUserDtos> Get(string connectionType,int id)
        {
            var item = ElectricityUserFactory.GetElectricityUserFactory(connectionType, _unityOfWork);
            return await item.GetElectricityUser(id);
        }

        // POST api/<ElectricityUserController>
        [HttpPost]
        [Route("users/connectionType/{connectionType}")]
        public async Task Post(string connectionType, [FromBody] ElectricityUserDtos value)
        {
            var item = ElectricityUserFactory.GetElectricityUserFactory(connectionType, _unityOfWork);
            await item.PostElectricityUser(value);
        }

        // PUT api/<ElectricityUserController>/5
        [HttpPut]
        [Route("users/connectionType/{connectionType}")]
        public async Task Put(string connectionType, [FromBody] ElectricityUserDtos value)
        {
            var item = ElectricityUserFactory.GetElectricityUserFactory(connectionType, _unityOfWork);
            await item.PutElectricityUser(value);
        }

        // DELETE api/<ElectricityUserController>/5
        [HttpDelete]
        [Route("users/connectionType/{connectionType}/id/{id}")]
        public async Task Delete(string connectionType, int id)
        {
            var item = ElectricityUserFactory.GetElectricityUserFactory(connectionType, _unityOfWork);
            await item.DeleteElectricityUser(id);
        }


    }
}
