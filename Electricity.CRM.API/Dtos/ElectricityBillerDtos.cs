namespace Electricity.CRM.API.Dtos
{
    public class ElectricityBillerDtos
    {
        public int Id { get; set; }
        public string ConnectionType { get; set; }
        public decimal Amount { get; set; }
        public int? CommercialUserId { get; set; }
        public int? ResidentialUserId { get; set; }
        public int? FlatUserId { get; set; }
        public int? FactoryUserId { get; set; }
    }

    public class ElectricityBillerReadDtos
    {
        public int Id { get; set; }
        public string ConnectionType { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
    }
}
