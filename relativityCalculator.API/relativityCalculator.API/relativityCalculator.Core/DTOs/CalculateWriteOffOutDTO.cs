using System.Runtime.Serialization;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Enums;
using System.Collections.Generic;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class CalculateWriteOffOutDTO : OUTBaseDTO
	{
		[DataMember(Name = "recommendation")]
		public string recommendation { get; set; }
		[DataMember(Name = "RecommendationList")]
		public List<string> RecommendationList { get; set; }
	}
}
