using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class AuditTrailInDTO
	{
		[DataMember(Name = "auditTrailId")]
		public string auditTrailId { get; set; }
		[DataMember(Name = "comment")]
		public string comment { get; set; }
	}
}
