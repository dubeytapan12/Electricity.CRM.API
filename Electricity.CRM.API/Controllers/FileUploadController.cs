using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Electricity.CRM.API.Dtos;
using System.Collections.Generic;
using Electricity.CRM.API.UnityOfWork;
using Electricity.CRM.API.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Electricity.CRM.API.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Electricity.CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        IConfiguration configuration;
        IWebHostEnvironment hostEnvironment;
        private readonly IUnityOfWork _unityOfWork;
        IExcelDataReader reader;
        private readonly IUserServiceRepository _userServiceRepository;

        public FileUploadController(IConfiguration configuration, IWebHostEnvironment hostEnvironment, IUnityOfWork unityOfWork, IUserServiceRepository userServiceRepository)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            _unityOfWork = unityOfWork;
            _userServiceRepository = userServiceRepository;
        }

        [HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> Index(IFormFile file,string userName)
        {
            try
            {
                // Check the File is received

                if (file == null)
                    throw new Exception("File is Not Received...");


                // Create the Directory if it is not exist
                string dirPath = Path.Combine(hostEnvironment.WebRootPath, "ReceivedReports");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                // MAke sure that only Excel file is used 
                string dataFileName = Path.GetFileName(file.FileName);
                List<ElectricityUserDtos> lstCommercial = new List<ElectricityUserDtos>();
                List<ElectricityUserDtos> lstResidential = new List<ElectricityUserDtos>();
                List<ElectricityUserDtos> lstFactory = new List<ElectricityUserDtos>();
                List<ElectricityUserDtos> lstflat = new List<ElectricityUserDtos>();
                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("Sorry! This file is not allowed, make sure that file having extension as either.xls or.xlsx is uploaded.");

                // Make a Copy of the Posted File from the Received HTTP Request
                string saveToPath = Path.Combine(dirPath, dataFileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // USe this to handle Encodeing differences in .NET Core
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                // read the excel file
                using (var stream = new FileStream(saveToPath, FileMode.Open))
                {
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        // Read the the Table
                        DataTable serviceDetails = ds.Tables[0];
                      
                        for (int i = 7; i < serviceDetails.Rows.Count; i++)
                        {
                            ElectricityUserDtos userDto = new ElectricityUserDtos();
                            userDto.FName = serviceDetails.Rows[i][1].ToString();
                            userDto.LName = serviceDetails.Rows[i][2].ToString();
                            userDto.ConnectionDate = DateTime.TryParse(serviceDetails.Rows[i][3].ToString(), out DateTime resultConnectionDate) ? resultConnectionDate : DateTime.Now;
                            userDto.Mobile = serviceDetails.Rows[i][4].ToString();
                            userDto.IsUsingNewMeeter = serviceDetails.Rows[i][5].ToString().ToLower()=="yes"? true : false;
                            userDto.FatherName = serviceDetails.Rows[i][6].ToString();
                            userDto.DOB =  DateTime.TryParse(serviceDetails.Rows[i][7].ToString(), out DateTime resultDob) ?  resultDob : DateTime.Now;
                            userDto.AadharNumber = serviceDetails.Rows[i][8].ToString();
                            userDto.AddressLine1 = serviceDetails.Rows[i][9].ToString();
                            userDto.AddressLine2 =serviceDetails.Rows[i][10].ToString();
                            userDto.City = serviceDetails.Rows[i][11].ToString();
                            userDto.District =serviceDetails.Rows[i][12].ToString();
                            userDto.state = serviceDetails.Rows[i][13].ToString();
                            userDto.Pincode = int.TryParse(serviceDetails.Rows[i][14].ToString(),out int rpincode) ? rpincode: 0;
                            userDto.Notes = serviceDetails.Rows[i][15].ToString();
                            
                            switch (serviceDetails.Rows[i][0].ToString().ToLower())
                            {
                                case "commercial":
                                    {
                                        lstCommercial.Add(userDto); 
                                        break;
                                    }
                                case "residential":
                                    {
                                        lstResidential.Add(userDto);
                                        break;
                                    }
                                case "flat":
                                    {
                                        lstflat.Add(userDto);
                                        break;
                                    }
                                case "factory":
                                    {
                                        lstFactory.Add(userDto);
                                        break;
                                    }

                            } 
                            
                        }

                        if(lstCommercial.Count> 0)
                        {
                        await _unityOfWork.ElectricityCommercialRepository.PostMultipleElectricityUser(lstCommercial);
                        }
                        if (lstFactory.Count > 0)
                        {
                            await _unityOfWork.ElectricityFactoryRepository.PostMultipleElectricityUser(lstFactory);
                        }
                        if (lstflat.Count > 0)
                        {
                            await _unityOfWork.ElectricityFlatRepository.PostMultipleElectricityUser(lstflat);
                        }
                        if (lstResidential.Count > 0)
                        {
                            await _unityOfWork.ElectricityResidentialRepository.PostMultipleElectricityUser(lstResidential);
                        }
                    }
                }
                var user = await _userServiceRepository.GetUserByUserName(userName);
                if (user == null)
                {
                    throw new System.Exception("Invalid user!");
                }
                if (string.IsNullOrEmpty(user.Email))
                {
                    throw new System.Exception("email not found for user!");
                }
                string message = $"uploaded and saved {(lstCommercial.Count + lstFactory.Count + lstflat.Count + lstResidential.Count)} Records, Total Commercial ${lstCommercial.Count} Records, Total Factory ${lstFactory.Count} Records, Total Flat ${lstflat.Count} Records, Total Residential ${lstResidential.Count} Records";
                string html = "<html><body><h1>Electricity CRM: Record Uploaded</h1> <br>"+ message+ "</body></html>";
                EmailService.Email(html, "user uploaded- Electricity CRM", user.Email);
                return Ok(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
