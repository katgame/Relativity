using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class AssessorInDTO
	{
		[DataMember(Name = "assessor")]
		public Assessor assessor { get; set; }
	}
}
