using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{
	public interface IRelativityLookUpRepository : IRepository<RelativityLookUp>, IAsyncRepository<RelativityLookUp>
	{
		IList<Relativities> GetRelativityByKey(IList<Relativities> key);
		IList<RelativityLookUp> GetRelativityById(string Id);
	}
}
