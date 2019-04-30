using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class RelativityLookUp : BaseEntity, IAggregateRoot
	{
		public string RelativityId { get; set; }
		public string RelativityKey { get; set; }
		public string RelativityValue { get; set; }
	}
}
