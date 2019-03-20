using System.Runtime.Serialization;
using relativityCalculator.Core.Models;
namespace relativityCalculator.Core.DTOs
{

	[DataContract]
	public class CalculateWriteOffInDTO
	{
		[DataMember(Name = "policyDetails")]
		public PolicyDetailsOutDTO policyDetails { get; set; }
	}
}
