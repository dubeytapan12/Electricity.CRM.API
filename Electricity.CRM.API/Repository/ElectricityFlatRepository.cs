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
    public class ElectricityFlatRepository : IElectricityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ElectricityFlatRepository(AppDbContext db, IMapper mapper)
        {
            this._context = db;
             _mapper = mapper;
        }
        public async Task DeleteElectricityUser(int id)
        {
            var electricityUserFlat = await _context.ElectricityUserFlat.FindAsync(id);
            if (electricityUserFlat == null)
            {
                throw new System.Exception("not found");
            }
            _context.ElectricityUserFlat.Remove(electricityUserFlat);
            await _context.SaveChangesAsync();

        }

        public bool ElectricityUserExists(int id)
        {
            return _context.ElectricityUserFlat.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<ElectricityUserDtos>> GetElectricityUsers()
        {
            return _mapper.Map<List<ElectricityUserDtos>>(await _context.ElectricityUserFlat.ToListAsync());
        }

        public async Task<ElectricityUserDtos> GetElectricityUser(int id)
        {
            return _mapper.Map<ElectricityUserDtos>(await _context.ElectricityUserFlat.FindAsync(id));
        }

        public async Task<ElectricityUserDtos> PostElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.ElectricityUserFlat.Add(_mapper.Map<ElectricityUserFlat>(electricityUser));
            await _context.SaveChangesAsync();
            return electricityUser;
        }

        public async Task PutElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.Entry(_mapper.Map<ElectricityUserFlat>(electricityUser)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task PostMultipleElectricityUser(List<ElectricityUserDtos> electricityUses)
        {
            _context.ElectricityUserFlat.AddRange(_mapper.Map<List<ElectricityUserFlat>>(electricityUses));
            await _context.SaveChangesAsync();
        }
    }
}
