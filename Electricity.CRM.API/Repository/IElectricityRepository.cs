using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public interface IElectricityRepository
    {
        Task<IEnumerable<ElectricityUserDtos>> GetElectricityUsers();
        Task<ElectricityUserDtos> GetElectricityUser(int id);
        Task PutElectricityUser(ElectricityUserDtos electricityUserCommercial);
        Task<ElectricityUserDtos> PostElectricityUser(ElectricityUserDtos electricityUserCommercial);
        Task DeleteElectricityUser(int id);
        bool ElectricityUserExists(int id);
    }
}
