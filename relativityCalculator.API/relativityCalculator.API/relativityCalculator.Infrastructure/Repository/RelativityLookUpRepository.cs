using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.Infrastructure.Repository
{
	public class RelativityLookUpRepository : EfRepository<RelativityLookUp>, IRelativityLookUpRepository
	{
		public RelativityLookUpRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}
	
		public IList<Relativities> GetRelativityByKey(IList<Relativities> keys)
		{
			foreach (var item in keys)
			{
				item.RelativityValue = Convert.ToDouble(_dbContext.RelativityLookUp.FirstOrDefault(x => x.RelativityKey == item.RequestValue
				&& x.RelativityId == item.Id.ToString())?.RelativityValue);
			}
			return keys;
		}

		public IList<RelativityLookUp> GetRelativityById(string Id)
		{
			return _dbContext.RelativityLookUp.Where(x => x.RelativityId == Id).ToList();
		}
	}
}
