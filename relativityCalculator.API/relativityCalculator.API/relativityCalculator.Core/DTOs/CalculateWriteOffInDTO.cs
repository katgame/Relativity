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

		[DataMember(Name = "vehicleDetail")]
		public VehicleDetail vehicleDetail { get; set; }
		[DataMember(Name = "userId")]
		public string userId { get; set; }
	}
}
