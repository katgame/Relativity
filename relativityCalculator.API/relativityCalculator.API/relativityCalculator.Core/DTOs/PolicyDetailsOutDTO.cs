using System.Runtime.Serialization;
using relativityCalculator.Core.Models;
using System.Collections.Generic;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class PolicyDetailsOutDTO : OUTBaseDTO
	{
		[DataMember(Name = "policyNumber")]
		public string policyNumber  { get; set; }

		[DataMember(Name = "vehicleDetail")]
		public List<VehicleDetail> vehicleDetail { get; set; }
	}
}
