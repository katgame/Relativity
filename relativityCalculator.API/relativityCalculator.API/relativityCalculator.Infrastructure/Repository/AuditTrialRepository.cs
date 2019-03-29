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

		public bool UpdateComment(string auditTrailId, string comment)
		{
			try
			{
				var trail = _dbContext.AuditTrail.SingleOrDefault(x => x.Id == Convert.ToInt32(auditTrailId));
				trail.Comments = comment;
				_dbContext.AuditTrail.Update(trail);
				_dbContext.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
