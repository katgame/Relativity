using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class AuditLog : BaseEntity, IAggregateRoot
	{
		public int? ItemId { get; set; }
		public DateTime? DateTimeStamp { get; set; }
		public string TableName { get; set; }
		public string Changes { get; set; }
		public string ValueBefore { get; set; }
		public string ValueAfter { get; set; }
		public string UserId { get; set; }
		public string UpdateReason { get; set; }
	}
}
