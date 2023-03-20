using System.ComponentModel.DataAnnotations.Schema;

namespace Electricity.CRM.API.Entity
{
    public class EducationOrCertification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TechnologyEnablementId { get; set; }

        [ForeignKey("TechnologyEnablementId")]
        public virtual TechnologyEnablement TechnologyEnablement { get; set; }
    }

    public class Languages
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public int TechnologyEnablementId { get; set; }

        [ForeignKey("TechnologyEnablementId")]
        public virtual TechnologyEnablement TechnologyEnablement { get; set; }
    }
    public class Skills
    {
        public int Id { get; set; }
        public string skill { get; set; }
        public int TechnologyEnablementId { get; set; }

        [ForeignKey("TechnologyEnablementId")]
        public virtual TechnologyEnablement TechnologyEnablement { get; set; }
    }
    public class Experience
    {
        public int Id { get; set; }
        public string ExperienceDetail { get; set; }
        public int TechnologyEnablementId { get; set; }

        [ForeignKey("TechnologyEnablementId")]
        public virtual TechnologyEnablement TechnologyEnablement { get; set; }
    }
    public class Client
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int TechnologyEnablementId { get; set; }

        [ForeignKey("TechnologyEnablementId")]
        public virtual TechnologyEnablement TechnologyEnablement { get; set; }
    }
}