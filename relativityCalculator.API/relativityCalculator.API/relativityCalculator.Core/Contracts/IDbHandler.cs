using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{
    public interface IDbHandler
    {
		PolicyDetailsOutDTO GetPolicyVehicleDetails(string policyNumber);
		Task<CalculateWriteOffOutDTO> CalculateClaim(CalculateWriteOffInDTO request);
	}
}
