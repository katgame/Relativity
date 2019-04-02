using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.Infrastructure.Repository
{
	public class RelativityRepository : EfRepository<Relativity>, IRelativityRepository
	{
		public RelativityRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}

		public IList<Relativities> GetActiveRelativities()
		{

			var result = new List<Relativities>();
			_dbContext.Relativity.Where(x => x.Active == true).ToList().ForEach(delegate (Relativity relativity)
			{
				result.Add(new Relativities
				{
					RelativityName = relativity.Name,
					Id = relativity.Id
				});
			});

			return result;
		}
	}
}
