using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
	public partial class CompanyType : BaseEntity, IAggregateRoot
	{
		public CompanyType()
		{
			Assessor = new HashSet<Assessor>();
		}

		public int Id { get; set; }
		public string CompanyTypeName { get; set; }
		public int? Percentage { get; set; }

		public ICollection<Assessor> Assessor { get; set; }
	}
}
