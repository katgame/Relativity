using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Exceptions;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Services
{
    public class CalculatorService : ICalculatorService
    {
		private IAssessorRepository _assessorRepository;
		private IRelativityRepository _relativityRepository;
		private IAreaRepository _areaRepository;
		private IDbHandler _idbhandler;
		internal ValidateClaim _claimCal;

        public CalculatorService(IDbHandler dbHandler, IAssessorRepository assessorRepository,
			IRelativityRepository relativityRepository,
			IAreaRepository areaRepository)
        {
            _idbhandler = dbHandler;
			_assessorRepository = assessorRepository;
			_relativityRepository = relativityRepository;
			_areaRepository = areaRepository;
			_claimCal = new ValidateClaim(relativityRepository, _areaRepository);
		}

		public PolicyDetailsOutDTO GetPolicyVehicleDetails(string policyNumber)
		{
			return  _idbhandler.GetPolicyVehicleDetails(policyNumber);
		}

		public void AddAssessor(Assessor assessor)
		{
			 _assessorRepository.Add(assessor);
		}

		public void UpdateAssessor(Assessor assessor)
		{
			 _assessorRepository.Update(assessor);
		}

		public async Task<CalculateWriteOffOutDTO> CalculateClaim(VehicleDetail request)
		{

			var result = _claimCal.validateRelativities(request);

			return null;
		}

		public IEnumerable<Assessor> GetAssessorList()
		{
			return _assessorRepository.ListAll();
		}

	}
}
