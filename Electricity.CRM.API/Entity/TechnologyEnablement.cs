using Microsoft.AspNetCore.Server.HttpSys;
using System.Collections.Generic;

namespace Electricity.CRM.API.Entity
{
    public class TechnologyEnablement
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobiles { get; set; }
        public string Emails { get; set; }
        public List<EducationOrCertification> EducationOrCertifications { get; set; }
        public List<Languages> Languages { get; set; }
        public string Background { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Experience> Experiencies { get; set; }
        public List<Client> Clients { get; set; }
    }
}
