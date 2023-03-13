using System.ComponentModel.DataAnnotations.Schema;

namespace Electricity.CRM.API.Entity
{
    public class ElectricityBiller
    {
        public int Id { get; set; }
        public string ConnectionType { get; set; }
        public decimal Amount { get; set; }
        public int? CommercialUserId { get; set; }
        public int? ResidentialUserId { get; set; }
        public int? FlatUserId { get; set; }
        public int? FactoryUserId { get; set; }

        [ForeignKey("CommercialUserId")]
        public virtual ElectricityUserCommercial ElectricityUserCommercial { get; set; }
        [ForeignKey("FactoryUserId")]
        public virtual ElectricityUserFactory ElectricityUserFactory { get; set; }
        [ForeignKey("FlatUserId")]
        public virtual ElectricityUserFlat ElectricityUserFlat { get; set; }
        [ForeignKey("ResidentialUserId")]
        public virtual ElectricityUserResidential ElectricityUserResidential { get; set; }
    }
}
