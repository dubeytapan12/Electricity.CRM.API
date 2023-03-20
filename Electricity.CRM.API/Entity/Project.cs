using System.ComponentModel.DataAnnotations.Schema;

namespace Electricity.CRM.API.Entity
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ClientName { get; set; }
        public int ResumeId { get; set; }
        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }
    }
}
