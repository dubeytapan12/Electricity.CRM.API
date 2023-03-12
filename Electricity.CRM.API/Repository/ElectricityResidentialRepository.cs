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
    public class ElectricityResidentialRepository : IElectricityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ElectricityResidentialRepository(AppDbContext db, IMapper mapper)
        {
            this._context = db;
            _mapper = mapper;
        }

        public async Task DeleteElectricityUser(int id)
        {
            var electricityUserResidential = await _context.ElectricityUserResidential.FindAsync(id);
            if (electricityUserResidential == null)
            {
                throw new System.Exception("not found");
            }
            _context.ElectricityUserResidential.Remove(electricityUserResidential);
            await _context.SaveChangesAsync();

        }

        public bool ElectricityUserExists(int id)
        {
            return _context.ElectricityUserResidential.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<ElectricityUserDtos>> GetElectricityUsers()
        {
            return _mapper.Map<List<ElectricityUserDtos>>(await _context.ElectricityUserResidential.ToListAsync());
        }

        public async Task<ElectricityUserDtos> GetElectricityUser(int id)
        {
            return _mapper.Map<ElectricityUserDtos>(await _context.ElectricityUserResidential.FindAsync(id));
        }

        public async Task<ElectricityUserDtos> PostElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.ElectricityUserResidential.Add(_mapper.Map<ElectricityUserResidential>(electricityUser));
            await _context.SaveChangesAsync();
            return electricityUser;
        }

        public async Task PutElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.Entry(_mapper.Map<ElectricityUserResidential>(electricityUser)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task PostMultipleElectricityUser(List<ElectricityUserDtos> electricityUses)
        {
            _context.ElectricityUserResidential.AddRange(_mapper.Map<List<ElectricityUserResidential>>(electricityUses));
            await _context.SaveChangesAsync();
        }
    }
}
