using System.Collections.Generic;

namespace Electricity.CRM.API.Entity
{
    public class Resume
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Skills { get; set; }

        public List<Project> projects { get; set; }
    }
}
