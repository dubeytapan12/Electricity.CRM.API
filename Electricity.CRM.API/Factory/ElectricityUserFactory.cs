using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Repository;
using Electricity.CRM.API.UnityOfWork;
using System.Collections.Generic;
using System;
using AutoMapper;

namespace Electricity.CRM.API.Factory
{
    public static class ElectricityUserFactory
    {
       
        public static IElectricityRepository GetElectricityUserFactory(string connectionType, IUnityOfWork unityOfWork)
        {
            switch (connectionType.ToLower())
            {
                case "commercial":
                    {
                        return unityOfWork.ElectricityCommercialRepository;
                    }
                case "residential":
                    {
                        return unityOfWork.ElectricityResidentialRepository;
                    }
                case "flat":
                    {
                        return unityOfWork.ElectricityFlatRepository;
                    }
                case "factory":
                    {
                        return unityOfWork.ElectricityFactoryRepository;
                    }
                default: throw new Exception("Not supported connection type");
            }
        }
    }
}
