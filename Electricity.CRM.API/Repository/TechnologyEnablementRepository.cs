using AutoMapper;
using Electricity.CRM.API.Context;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public class TechnologyEnablementRepository : ITechnologyEnablementRepository
    {
        private readonly AppDbContext _context;
       
        public TechnologyEnablementRepository(AppDbContext db)
        {
            this._context = db;
        }
        public async Task PostTechnologyEnablement(TechnologyEnablement technologyEnablement)
        {
            try
            {
                _context.TechnologyEnablement.Add(technologyEnablement);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                string e = ex.Message;
            }
        }
    }
}
