using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.Infrastructure.Repository
{
	public class RelativityRepository : EfRepository<RelativityLookUp>, IRelativityRepository
	{
		public RelativityRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}
		public IList<string> GetActiveRelativities()
		{
			return _dbContext.RelativityLookUp.GroupBy(x => x.RelativityName).
				Select(group => group.First()).Select(x => x.RelativityName).ToList();
		}
		public IList<Relativity> GetRelativityByKey(IList<Relativity> keys)
		{
			foreach (var item in keys)
			{
				item.RelativityValue = Convert.ToString(_dbContext.RelativityLookUp.FirstOrDefault(x => x.RelativityKey == item.RequestValue
				&& x.RelativityName == item.RelativityName)?.RelativityValue);
			}
			return keys;
		}

		public IList<RelativityLookUp> GetRelativityByName(string Name)
		{
			return _dbContext.RelativityLookUp.Where(x => x.RelativityName == Name).ToList();
		}
	}
}
