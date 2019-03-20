using System.Runtime.Serialization;

namespace relativityCalculator.Core.DTOs
{

	[DataContract(Namespace = "")]
	public class OUTBaseDTO
	{
		[DataMember]
		public bool bSuccess { get; set; }

		//[DataMember]
		//public string channelCode { get; set; }

		[DataMember]
		public string Errors { get; set; }
	}
}
