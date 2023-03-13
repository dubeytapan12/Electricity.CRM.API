using Electricity.CRM.API.Context;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public class ElectricityBillerRepository : IElectricityBillerRepository
    {
        private readonly AppDbContext _context;
        public ElectricityBillerRepository(AppDbContext db)
        {
            this._context = db;
        }
        public async Task AddBillingInformation(ElectricityBiller item)
        {
            _context.ElectricityBiller.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<BillerGroupDtos> GetGroupingBill()
        {
          var result=   await _context.ElectricityBiller.GroupBy(u => u.ConnectionType).Select(g => new 
            {
               key=  g.Key,
                SUM = g.Select(s => s.Amount).Sum()
            }).ToListAsync();

            return new BillerGroupDtos() { ConnectionTypes = result.Select(u => u.key).ToArray(), TotalAmounts = result.Select(u => u.SUM).ToArray() };
        }
    }
}
