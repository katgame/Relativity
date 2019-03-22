using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.Infrastructure.Repository
{
	public class AreaRepository : EfRepository<Assessor>, IAreaRepository
	{

		public AreaRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}
		public string GetArea(string postalCode)
		{
			return Convert.ToString(_dbContext.AreasLookUp.FirstOrDefault(x => x.PostalCode == postalCode)?.Area);
		}
	}
}
