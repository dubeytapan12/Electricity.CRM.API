using Electricity.CRM.API.Context;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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


        public async Task<List<ElectricityBillerReadDtos>> GetBillers(string connectionType)
        {

            switch (connectionType)
            {
                case "commercial":
                    {
                        return await _context.ElectricityBiller.Include(u => u.ElectricityUserCommercial).Where(u=> u.CommercialUserId !=null).Select(u => new ElectricityBillerReadDtos()
                        {
                            Amount = u.Amount,
                            ConnectionType = u.ConnectionType,
                            Name = u.ElectricityUserCommercial.FName + " " + u.ElectricityUserCommercial.LName
                        }).ToListAsync();
                    }
                case "residential":
                    {
                        return await _context.ElectricityBiller.Include(u => u.ElectricityUserResidential).Where(u => u.ResidentialUserId != null).Select(u => new ElectricityBillerReadDtos()
                        {
                            Amount = u.Amount,
                            ConnectionType = u.ConnectionType,
                            Name = u.ElectricityUserResidential.FName + " " + u.ElectricityUserResidential.LName
                        }).ToListAsync();
                    }
                case "factory":
                    {
                        return await _context.ElectricityBiller.Include(u => u.ElectricityUserFactory).Where(u => u.FactoryUserId != null).Select(u => new ElectricityBillerReadDtos()
                        {
                            Amount = u.Amount,
                            ConnectionType = u.ConnectionType,
                            Name = u.ElectricityUserFactory.FName + " " + u.ElectricityUserFactory.LName
                        }).ToListAsync();
                    }
                case "flat":
                    {
                        return await _context.ElectricityBiller.Include(u => u.ElectricityUserFlat).Where(u => u.FlatUserId != null).Select(u => new ElectricityBillerReadDtos()
                        {
                            Amount = u.Amount,
                            ConnectionType = u.ConnectionType,
                            Name = u.ElectricityUserFlat.FName + " " + u.ElectricityUserFlat.LName
                        }).ToListAsync();
                    }
                default:throw new System.Exception("Invalid connection Type, Please send currect connection type");
            }
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
