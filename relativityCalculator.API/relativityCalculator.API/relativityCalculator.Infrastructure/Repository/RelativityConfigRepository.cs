using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using System.Linq;
using relativityCalculator.Core.Contracts;

namespace relativityCalculator.Infrastructure.Repository
{
	public class RelativityConfigRepository : EfRepository<RelativityConfig>, IRelativityConfig
	{
		public RelativityConfigRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}

		public string GetConfigValue(string propertyName)
		{
			return _dbContext.RelativityConfig.SingleOrDefault(x => x.PropertyName == propertyName)?.PropertyValue;
		}
	}
}
