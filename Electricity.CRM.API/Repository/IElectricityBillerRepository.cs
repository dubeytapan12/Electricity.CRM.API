using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public interface IElectricityBillerRepository
    {
        Task AddBillingInformation(ElectricityBiller item);
        Task<BillerGroupDtos> GetGroupingBill();

        Task<List<ElectricityBillerReadDtos>> GetBillers(string connectionType);
    }
}
