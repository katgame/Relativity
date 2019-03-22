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
		public int GetCompanyPercentage(int assessorId)
		{
			return Convert.ToInt32(_dbContext.Assessor.SingleOrDefault(x => x.Id == assessorId).CompanyTypeNavigation.Percentage);
		}

	}
}
