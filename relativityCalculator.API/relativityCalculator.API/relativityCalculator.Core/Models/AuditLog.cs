using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class AuditLog : BaseEntity, IAggregateRoot
	{
		public int Id { get; set; }
		public int? AuditAction { get; set; }
		public DateTime? DateTimeStamp { get; set; }
		public string DataModel { get; set; }
		public string Changes { get; set; }
		public string ValueBefore { get; set; }
		public string ValueAfter { get; set; }
	}
}
