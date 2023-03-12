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
    public class ElectricityFactoryRepository : IElectricityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ElectricityFactoryRepository(AppDbContext db, IMapper mapper)
        {
            this._context = db;
            _mapper = mapper;
        }
        public async Task DeleteElectricityUser(int id)
        {
            var electricityUserFactory = await _context.ElectricityUserFactory.FindAsync(id);
            if (electricityUserFactory == null)
            {
                throw new System.Exception("not found");
            }
            _context.ElectricityUserFactory.Remove(electricityUserFactory);
            await _context.SaveChangesAsync();

        }

        public bool ElectricityUserExists(int id)
        {
            return _context.ElectricityUserFactory.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<ElectricityUserDtos>> GetElectricityUsers()
        {
            return _mapper.Map<List<ElectricityUserDtos>>(await _context.ElectricityUserFactory.ToListAsync());
        }

        public async Task<ElectricityUserDtos> GetElectricityUser(int id)
        {
            return _mapper.Map<ElectricityUserDtos>(await _context.ElectricityUserFactory.FindAsync(id));
        }

        public async Task<ElectricityUserDtos> PostElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.ElectricityUserFactory.Add(_mapper.Map<ElectricityUserFactory>(electricityUser));
            await _context.SaveChangesAsync();
            return electricityUser;
        }

        public async Task PutElectricityUser(ElectricityUserDtos electricityUser)
        {
            _context.Entry(_mapper.Map<ElectricityUserFactory>(electricityUser)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task PostMultipleElectricityUser(List<ElectricityUserDtos> electricityUses)
        {
            _context.ElectricityUserFactory.AddRange(_mapper.Map<List<ElectricityUserFactory>>(electricityUses));
            await _context.SaveChangesAsync();
        }
    }
}
