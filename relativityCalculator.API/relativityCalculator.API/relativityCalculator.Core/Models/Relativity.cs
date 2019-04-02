using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
	public class Relativity : BaseEntity, IAggregateRoot
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Active { get; set; }
	}
}
