using AutoMapper;
using Electricity.CRM.API.Context;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public class ElectricityCommercialRepository : IElectricityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ElectricityCommercialRepository(AppDbContext db, IMapper mapper)
        {
            this._context = db;
            _mapper = mapper;
        }
        public async Task DeleteElectricityUser(int id)
        {
            var electricityUserCommercial = await _context.ElectricityUserCommercial.FindAsync(id);
            if (electricityUserCommercial == null)
            {
                throw new System.Exception("not found");
            }
            _context.ElectricityUserCommercial.Remove(electricityUserCommercial);
            await _context.SaveChangesAsync();

        }

        public bool ElectricityUserExists(int id)
        {
            return _context.ElectricityUserCommercial.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<ElectricityUserDtos>> GetElectricityUsers()
        {
            return  _mapper.Map<List<ElectricityUserDtos>>(await _context.ElectricityUserCommercial.ToListAsync());
        }

        public async Task<ElectricityUserDtos> GetElectricityUser(int id)
        {
            return _mapper.Map<ElectricityUserDtos>(await _context.ElectricityUserCommercial.FindAsync(id));
        }

        public async Task<ElectricityUserDtos> PostElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.ElectricityUserCommercial.Add(_mapper.Map<ElectricityUserCommercial>(electricityUser));
            await _context.SaveChangesAsync();
            return electricityUser;
        }

        public async Task PutElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.Entry(_mapper.Map<ElectricityUserCommercial>(electricityUser)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task PostMultipleElectricityUser(List<ElectricityUserDtos> electricityUses)
        {
            _context.ElectricityUserCommercial.AddRange(_mapper.Map<List<ElectricityUserCommercial>>(electricityUses));
            await _context.SaveChangesAsync();
        }
    }
}
