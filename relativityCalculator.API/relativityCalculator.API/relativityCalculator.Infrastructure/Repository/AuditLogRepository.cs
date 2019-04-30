using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.Infrastructure.Repository
{
	public class AuditLogRepository : EfRepository<AuditLog>, IAuditLogRepository
	{

		public AuditLogRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}
	}
}
