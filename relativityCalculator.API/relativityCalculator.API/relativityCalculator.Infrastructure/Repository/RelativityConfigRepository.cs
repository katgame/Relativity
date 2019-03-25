using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using System.Linq;
using relativityCalculator.Core.Contracts;
using System;

namespace relativityCalculator.Infrastructure.Repository
{
	public class RelativityConfigRepository : EfRepository<RelativityConfig>, IRelativityConfig
	{
		public RelativityConfigRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}

		public double GetConfigValue(string propertyName)
		{
			return Convert.ToDouble(_dbContext.RelativityConfig.SingleOrDefault(x => x.PropertyName == propertyName)?.PropertyValue);
		}
	}
}
