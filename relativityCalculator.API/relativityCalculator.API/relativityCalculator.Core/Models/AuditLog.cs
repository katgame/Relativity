using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class AuditLog : BaseEntity, IAggregateRoot
	{
        public int Id { get; set; }
        public int? KeyFieldId { get; set; }
        public int? AuditActionTypeEnum { get; set; }
        public DateTime? DateTimeStamp { get; set; }
        public byte[] DataModel { get; set; }
        public byte[] Changes { get; set; }
        public byte[] ValueBefore { get; set; }
        public byte[] ValueAfter { get; set; }
    }
}
