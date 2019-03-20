using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{
	public interface IRelativityRepository : IRepository<RelativityLookUp>, IAsyncRepository<RelativityLookUp>
	{
		IList<Relativity> GetRelativityByKey(IList<Relativity> key);
		IList<RelativityLookUp> GetRelativityByName(string Name);
		IList<string> GetActiveRelativities();

	}
}
