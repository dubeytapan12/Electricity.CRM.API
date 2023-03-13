using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electricity.CRM.API.Context;
using Electricity.CRM.API.Entity;
using Electricity.CRM.API.Repository;
using Electricity.CRM.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Electricity.CRM.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ElectricityBillersController : ControllerBase
    {
        private readonly IElectricityBillerRepository _electricityBillerRepository;
        private readonly IMapper _mapper;

        public ElectricityBillersController(IElectricityBillerRepository electricityBillerRepository, IMapper mapper)
        {
            _electricityBillerRepository = electricityBillerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("get-bill-grouping")]
        public async Task<ActionResult<BillerGroupDtos>> Get()
        {
            return Ok(await _electricityBillerRepository.GetGroupingBill());
        }
        // POST: api/ElectricityBillers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("save-bill")]
        public async Task<ActionResult<ElectricityBillerDtos>> PostElectricityBiller(ElectricityBillerDtos electricityBiller)
        {
            await _electricityBillerRepository.AddBillingInformation(_mapper.Map<ElectricityBiller>(electricityBiller));

            return Ok("saved");
        }

        
    }
}
