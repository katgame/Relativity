using System.Runtime.Serialization;
using relativityCalculator.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class UpdateRelativityInDTO
	{
		[DataMember(Name = "userId")]
		public int UserId { get; set; }
		[DataMember(Name = "reason")]
		public string UpdateReason { get; set; }
		[DataMember(Name = "relativity")]
		[Required]
		public RelativityLookUp relativity { get; set; }
	}
}
