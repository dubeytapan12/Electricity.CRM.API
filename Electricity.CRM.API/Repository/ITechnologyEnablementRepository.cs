using Electricity.CRM.API.Entity;
using System.Threading.Tasks;

namespace Electricity.CRM.API.Repository
{
    public interface ITechnologyEnablementRepository
    {
        Task PostTechnologyEnablement(TechnologyEnablement technologyEnablement);
    }
}
