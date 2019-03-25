using System.Runtime.Serialization;
using relativityCalculator.Core.Models;
using System.Collections.Generic;

namespace relativityCalculator.Core.DTOs
{

	[DataContract]
	public class CalculateWriteOffInDTO
	{
		[DataMember(Name = "policyNumber")]
		public string policyNumber { get; set; }

		[DataMember(Name = "claimNumber")]
		public string claimNumber { get; set; }

		[DataMember(Name = "vehicleDetail")]
		public VehicleDetail vehicleDetail { get; set; }
		[DataMember(Name = "assessorId")]
		public string assessorId { get; set; }
	}
}
