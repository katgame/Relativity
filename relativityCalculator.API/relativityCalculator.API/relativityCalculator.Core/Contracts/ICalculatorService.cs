﻿using System;
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
		Task<CalculateWriteOffOutDTO> CalculateClaim(VehicleDetail request);
		void AddAssessor(Assessor assessor);
		void UpdateAssessor(Assessor assessor);
		IEnumerable<Assessor> GetAssessorList();


	}
}