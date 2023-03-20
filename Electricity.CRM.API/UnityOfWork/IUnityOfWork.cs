using Electricity.CRM.API.Repository;

namespace Electricity.CRM.API.UnityOfWork
{
    public interface IUnityOfWork
    {
        IElectricityRepository ElectricityCommercialRepository { get; }
        IElectricityRepository ElectricityResidentialRepository { get; }
        IElectricityRepository ElectricityFactoryRepository { get; }
        IElectricityRepository ElectricityFlatRepository { get; }
        ITechnologyEnablementRepository TechnologyEnablementRepository { get; }
        IResumeRepository ResumeRepository { get; }
    }
}
