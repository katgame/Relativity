using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Models;
namespace relativityCalculator.Core.Contracts
{
	public interface IRelativityConfig : IRepository<RelativityConfig>, IAsyncRepository<RelativityConfig>
	{
		string GetConfigValue(string propertyName);
	}
}
