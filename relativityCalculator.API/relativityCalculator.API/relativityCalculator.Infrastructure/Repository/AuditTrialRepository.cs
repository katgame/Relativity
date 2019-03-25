using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.Infrastructure.Repository
{
	public class AuditTrialRepository : EfRepository<AuditTrail>, IAuditTrailRepository
	{

		public AuditTrialRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}
	}
}
