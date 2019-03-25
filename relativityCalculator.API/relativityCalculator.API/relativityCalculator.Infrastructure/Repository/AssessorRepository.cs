using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;


namespace relativityCalculator.Infrastructure.Repository
{
	public class AssessorRepository : EfRepository<Assessor>, IAssessorRepository
	{

		public AssessorRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}
		public double GetCompanyPercentage(int assessorId)
		{
			var assessor = _dbContext.Assessor.SingleOrDefault(x => x.Id == assessorId);
			return Convert.ToInt32(_dbContext.CompanyType.SingleOrDefault(x => x.Id == assessor.CompanyTypeId).Percentage);
		}

	}
}
