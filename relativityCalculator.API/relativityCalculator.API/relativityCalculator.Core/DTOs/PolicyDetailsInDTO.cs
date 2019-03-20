using System.Runtime.Serialization;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class PolicyDetailsInDTO
	{
		/// <summary>
		/// Policy Number
		/// </summary>
		[DataMember(Name = "policyNumber")]
		public string PolicyNumber { get; set; }
	}
}
