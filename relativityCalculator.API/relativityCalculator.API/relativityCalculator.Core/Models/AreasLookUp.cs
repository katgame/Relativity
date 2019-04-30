using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class AreasLookUp : BaseEntity, IAggregateRoot
	{
        public string PostalCode { get; set; }
        public string Area { get; set; }
    }
}
