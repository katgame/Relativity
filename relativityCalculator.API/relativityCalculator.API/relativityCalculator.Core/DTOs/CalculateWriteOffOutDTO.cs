using System.Runtime.Serialization;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class CalculateWriteOffOutDTO : OUTBaseDTO
	{
		public string recommendation { get; set; }
	}
}
