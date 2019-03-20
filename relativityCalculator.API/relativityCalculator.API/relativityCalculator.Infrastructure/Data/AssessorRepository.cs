using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Infrastructure.Models;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;


namespace relativityCalculator.Infrastructure.Data
{
	public class AssessorRepository : EfRepository<Assessor>, IAssessorRepository
	{

		public AssessorRepository(RelativitiesContext dbContext) : base(dbContext)
		{
		}

	}
}
