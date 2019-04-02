using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{
	public interface IRelativityRepository : IRepository<Relativity>, IAsyncRepository<Relativity>
	{
		IList<Relativities> GetActiveRelativities();
	}
}
