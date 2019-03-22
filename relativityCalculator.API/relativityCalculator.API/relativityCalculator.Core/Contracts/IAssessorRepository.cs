using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using  relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{
	public interface IAssessorRepository : IRepository<Assessor>, IAsyncRepository<Assessor>
	{
		int GetCompanyPercentage(int assessorId);
	
	}
}
