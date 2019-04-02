using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Models;
using System.Threading.Tasks;

namespace relativityCalculator.Core.Contracts
{
    public interface ICalculatorService
    {
        PolicyDetailsOutDTO GetPolicyVehicleDetails(string policyNumber);
		Task<CalculateWriteOffOutDTO> CalculateClaim(CalculateWriteOffInDTO request);
		void AddAssessor(Assessor assessor);
		void UpdateAssessor(Assessor assessor);
		bool UpdateAuditTrail(string claimNumber, string comments);
		IEnumerable<Assessor> GetAssessorList();
		Task<IList<Relativity>> GetRelativityGroup();
		Task<IList<RelativityLookUp>> GetRelativityByGroupId(string relativityId);

	}
}
