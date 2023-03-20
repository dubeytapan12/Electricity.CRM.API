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
using Syncfusion.Presentation;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

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
        [Route("ProjectFile/{userName}")]
        public async Task<IActionResult> ProjectFile(IFormFile file, string userName)
        {
            if (file == null)
                throw new Exception("File is Not Received...");
            // Create the Directory if it is not exist
            string dirPath = Path.Combine(hostEnvironment.WebRootPath, "PrjectedReports");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string dataFileName =  DateTime.Now.Ticks+"."+Path.GetFileName(file.FileName).Split(".")[1];
            string extension = Path.GetExtension(dataFileName);
            string[] allowedExtsnions = new string[] { ".pptx",".docx","doc" };

            if (!allowedExtsnions.Contains(extension))
                throw new Exception("Sorry! This file is not allowed, make sure that file having extension as either.xls or.xlsx is uploaded.");

            // Make a Copy of the Posted File from the Received HTTP Request
            string saveToPath = Path.Combine(dirPath, dataFileName);

            using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
           if(extension==".docx" || extension == ".doc")
            {
              await  ReadAndSaveWordFile(saveToPath);
            }
            if (extension == ".pptx")
            {
              await  ReadAndSavePPT(saveToPath);
              
            }
            return Ok("file uploaded");
        }

        private async Task ReadAndSaveWordFile(string saveToPath)
        {
            using (FileStream inputFileStream = new FileStream(saveToPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //Load the file stream into a Word document.
                using (WordDocument document = new WordDocument(inputFileStream, Syncfusion.DocIO.FormatType.Automatic))
                {
                    //Get the Word document text.
                    var conent = document.GetText().Split("\r");
                    var resume = new Resume();
                    resume.Name = conent[1].Split(":")[1];
                    resume.MobileNumber = conent[2].Split(":")[1];
                    resume.EmailId = conent[3].Split(":")[1];
                    int projectDetailsIndex = 0;
                    for (int i = 5; i < conent.Length; i++)
                    {
                        if (conent[i].Replace("\n", "").Trim() != string.Empty && !conent[i].Replace("\n", "").Trim().ToLower().Contains("syncfusion word"))
                        {
                            if (conent[i].Replace("\n", "").Trim() == "Project Details:")
                            {
                                projectDetailsIndex = i + 1;
                                break;
                            }
                            resume.Skills += conent[i].Replace("\n", "") + ",";
                        }
                    }
                    resume.Skills = resume.Skills.Substring(0, resume.Skills.Length - 1);
                    List<Project> projects = new List<Project>();
                    for (int i = projectDetailsIndex; i < conent.Length; i++)
                    {
                        if (conent[i].Replace("\n", "").Trim() != string.Empty && !conent[i].Replace("\n", "").Trim().ToLower().Contains("syncfusion word"))
                        {
                            projects.Add(new Project() { Name = conent[i].Replace("\n", "").Split(":")[1], StartDate = conent[i + 1].Replace("\n", "").Split(":")[1], EndDate = conent[i + 2].Replace("\n", "").Split(":")[1], ClientName = conent[i + 3].Replace("\n", "").Split(":")[1] });
                            i = i + 3;
                        }
                    }
                    resume.projects = projects;

                    await _unityOfWork.ResumeRepository.PostResumeRepository(resume);
                }
            }
        }
        private async Task ReadAndSavePPT(string saveToPath)
        {
            IPresentation pptxDoc = Presentation.Open(new FileStream(saveToPath, FileMode.Open));
            //Gets the first slide from the PowerPoint presentation
            ISlide slide = pptxDoc.Slides[0];

            //Gets the first shape of the slide
            try
            {
                TechnologyEnablement technologyEnablement = new TechnologyEnablement();
                technologyEnablement.FullName = (slide.Shapes[1] as IShape).TextBody.Text;
                ReadLeftSideContents(slide, technologyEnablement);
                ReadRightSideContents(slide, technologyEnablement);
                //looping left side content
               await _unityOfWork.TechnologyEnablementRepository.PostTechnologyEnablement(technologyEnablement);
                pptxDoc.Close();
            }
            catch (Exception ex)
            {
                pptxDoc.Close();
                throw new Exception("Invalid PPT Format, Please upload only approved PPT");
            }
            //Close the PowerPoint presentation
            pptxDoc.Dispose();


        }
        private void ReadRightSideContents(ISlide slide, TechnologyEnablement technologyEnablement)
        {
            bool isBackgroundExists = false;
            bool isSkillsExists = false;
            bool isExperienceExists = false;
            bool isClientExists = false;
            foreach (string leftContent in (slide.Shapes[3] as IShape).TextBody.Text.Split("\r"))
            {
                if (leftContent.Trim() == string.Empty) { continue; }
                if (leftContent.Contains("Background"))
                {
                    isBackgroundExists = true;
                    continue;
                }
                if (isBackgroundExists == true)
                {
                    if (leftContent.Contains("Below are some skills"))
                    {
                        isBackgroundExists = false;
                        isSkillsExists = true;
                        continue;
                    }
                    technologyEnablement.Background += leftContent + " ";
                }
                if (isSkillsExists)
                {
                    if (leftContent.Trim() == "Experience")
                    {
                        isSkillsExists = false;
                        isExperienceExists = true;
                        continue;
                    }
                    if (technologyEnablement.Skills == null) { technologyEnablement.Skills = new List<Skills>(); }
                    technologyEnablement.Skills.Add(new Skills() { skill = leftContent });
                }
                if (isExperienceExists)
                {
                    if (leftContent.Trim().Contains("Representative clients"))
                    {
                        isExperienceExists = false;
                        isClientExists = true;
                        continue;
                    }
                    if (technologyEnablement.Experiencies == null) { technologyEnablement.Experiencies = new List<Experience>(); }
                    technologyEnablement.Experiencies.Add(new Experience() { ExperienceDetail = leftContent });
                }
                if (isClientExists)
                {
                    if (technologyEnablement.Clients == null) { technologyEnablement.Clients = new List<Client>(); }
                    technologyEnablement.Clients.Add(new Client() { ClientName = leftContent });
                }
            }
        }
        private void ReadLeftSideContents(ISlide slide, TechnologyEnablement technologyEnablement)
        {
            bool isEducationCertificationListExists = false;
            bool isLanguageListExists = false;
            List<EducationOrCertification> educationOrCertifications = new List<EducationOrCertification>();
            List<Languages> languages = new List<Languages>();
            foreach (string leftContent in (slide.Shapes[2] as IShape).TextBody.Text.Split("\r"))
            {
                if (leftContent.Contains("M:-") || leftContent.Contains("Mobile:-") || leftContent.Contains("m:-") || leftContent.Contains("mobile:-")) //Mobile
                {
                    technologyEnablement.Mobiles += leftContent.Split(":-")[1] + ",";
                }
                if (leftContent.Contains("E:-") || leftContent.Contains("Email:-") || leftContent.Contains("e:-") || leftContent.Contains("email:-")) //email
                {
                    technologyEnablement.Emails += leftContent.Split(":-")[1] + ",";
                }
                if (!(leftContent.Contains("E:-") || leftContent.Contains("Email:-") || leftContent.Contains("e:-") || leftContent.Contains("email:-")) && leftContent.Contains("@")) //email
                {
                    technologyEnablement.Emails += leftContent + ",";
                }

                if (leftContent.Contains("Education/Certifications"))
                {
                    isEducationCertificationListExists = true;
                    continue;
                }
                if (isEducationCertificationListExists)
                {
                    if (!leftContent.Contains("Language") && leftContent.Trim() != string.Empty)
                    {
                        educationOrCertifications.Add(new EducationOrCertification() { Name = leftContent });
                    }
                    else
                    {
                        isEducationCertificationListExists = false;
                    }
                }
                if (leftContent.Contains("Language"))
                {
                    isLanguageListExists = true;
                    continue;
                }
                if (isLanguageListExists)
                {
                    if (leftContent.Trim() != string.Empty)
                    {
                        languages.Add(new Languages() { Language = leftContent });
                    }
                }
            }
            technologyEnablement.Emails = technologyEnablement.Emails.Substring(0, technologyEnablement.Emails.Length - 1);
            technologyEnablement.Mobiles = technologyEnablement.Mobiles.Substring(0, technologyEnablement.Mobiles.Length - 1);
            technologyEnablement.EducationOrCertifications = educationOrCertifications;
            technologyEnablement.Languages = languages;
        }
        [HttpPost]
        [Route("{userName}")]
        public async Task<IActionResult> Index(IFormFile file, string userName)
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
                            userDto.IsUsingNewMeeter = serviceDetails.Rows[i][5].ToString().ToLower() == "yes" ? true : false;
                            userDto.FatherName = serviceDetails.Rows[i][6].ToString();
                            userDto.DOB = DateTime.TryParse(serviceDetails.Rows[i][7].ToString(), out DateTime resultDob) ? resultDob : DateTime.Now;
                            userDto.AadharNumber = serviceDetails.Rows[i][8].ToString();
                            userDto.AddressLine1 = serviceDetails.Rows[i][9].ToString();
                            userDto.AddressLine2 = serviceDetails.Rows[i][10].ToString();
                            userDto.City = serviceDetails.Rows[i][11].ToString();
                            userDto.District = serviceDetails.Rows[i][12].ToString();
                            userDto.state = serviceDetails.Rows[i][13].ToString();
                            userDto.Pincode = int.TryParse(serviceDetails.Rows[i][14].ToString(), out int rpincode) ? rpincode : 0;
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

                        if (lstCommercial.Count > 0)
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
                string html = "<html><body><h1>Electricity CRM: Record Uploaded</h1> <br>" + message + "</body></html>";
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
