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
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _context;
       
        public ResumeRepository(AppDbContext db)
        {
            this._context = db;
        }
        public async Task PostResumeRepository(Resume resume)
        {
            try
            {
                _context.Resume.Add(resume);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                string e = ex.Message;
            }
        }
    }
}
