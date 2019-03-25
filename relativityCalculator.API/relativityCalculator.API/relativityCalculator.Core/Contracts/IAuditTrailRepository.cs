using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Models;
namespace relativityCalculator.Core.Contracts
{
	public interface IAuditTrailRepository : IRepository<AuditTrail>, IAsyncRepository<AuditTrail>
	{
		
	}
}
