using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class AuditTrail : BaseEntity, IAggregateRoot
	{
        public int Id { get; set; }
        public string ClaimNumer { get; set; }
        public string IncidentYear { get; set; }
        public string VehicleSumInsured { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMass { get; set; }
        public string Area { get; set; }
        public string RepairCost { get; set; }
        public string AdditionalCosts { get; set; }
        public string TotalCostRepair { get; set; }
        public string ExpectedSalvageRecovery { get; set; }
        public string CostOfSalvage { get; set; }
        public string TotalSalvageRecovery { get; set; }
        public string DifferenceInCost { get; set; }
        public string Recommendation { get; set; }
        public string Comments { get; set; }
    }
}
