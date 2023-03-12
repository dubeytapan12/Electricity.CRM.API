using AutoMapper;
using Electricity.CRM.API.Context;
using Electricity.CRM.API.Repository;

namespace Electricity.CRM.API.UnityOfWork
{
    //implemented unity of work as mentioned in requirement
    public class UnityOfWorker : IUnityOfWork
    {
        private readonly AppDbContext _db;
        private IElectricityRepository _electricityCommercialRepository;
        private IElectricityRepository _electricityResidentialRepository;
        private IElectricityRepository _electricityFactoryRepository;
        private IElectricityRepository _electricityFlatRepository;
        private readonly IMapper _mapper;

        public UnityOfWorker(AppDbContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        
        public IElectricityRepository ElectricityCommercialRepository
        {
            get
            {
                if(_electricityCommercialRepository==null)
                {
                    _electricityCommercialRepository = new ElectricityCommercialRepository(_db, _mapper);
                }
                return _electricityCommercialRepository;
            }
        }
        public IElectricityRepository ElectricityResidentialRepository
        {
            get
            {
                if (_electricityResidentialRepository == null)
                {
                    _electricityResidentialRepository = new ElectricityResidentialRepository(_db, _mapper);
                }
                return _electricityResidentialRepository;
            }
        }
        public IElectricityRepository ElectricityFactoryRepository
        {
            get
            {
                if (_electricityFactoryRepository == null)
                {
                    _electricityFactoryRepository = new ElectricityFactoryRepository(_db, _mapper);
                }
                return _electricityFactoryRepository;
            }
        }
        public IElectricityRepository ElectricityFlatRepository
        {
            get
            {
                if (_electricityFlatRepository == null)
                {
                    _electricityFlatRepository = new ElectricityFlatRepository(_db, _mapper);
                }
                return _electricityFlatRepository;
            }
        }
    }
}
