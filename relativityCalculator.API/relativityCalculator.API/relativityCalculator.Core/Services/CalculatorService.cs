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
		private IRelativityConfig _relativityConfig;
		private IDbHandler _idbhandler;
		private IAuditTrailRepository _auditTrailRepository;
		internal ValidateClaim _claimCal;

        public CalculatorService(IDbHandler dbHandler,
			IAssessorRepository assessorRepository,
			IRelativityRepository relativityRepository,
			IAreaRepository areaRepository,
			IRelativityConfig relativityConfig,
			IAuditTrailRepository auditTrailRepository)
        {
            _idbhandler = dbHandler;
			_assessorRepository = assessorRepository;
			_relativityRepository = relativityRepository;
			_areaRepository = areaRepository;
			_relativityConfig = relativityConfig;
			_auditTrailRepository = auditTrailRepository;
			_claimCal = new ValidateClaim(relativityRepository,
				_areaRepository, 
			    _relativityConfig,
				_assessorRepository,
				_auditTrailRepository);
		}

		public PolicyDetailsOutDTO GetPolicyVehicleDetails(string policyNumber)
		{
			return  _idbhandler.GetPolicyVehicleDetails(policyNumber);
		}

		public PolicyDetailsOutDTO UpdateComment(string policyNumber)
		{
			return _idbhandler.GetPolicyVehicleDetails(policyNumber);
		}
		public void AddAssessor(Assessor assessor)
		{
			 _assessorRepository.Add(assessor);
			 
		}

		public void UpdateAssessor(Assessor assessor)
		{
			 _assessorRepository.Update(assessor);
		}

		public async Task<CalculateWriteOffOutDTO> CalculateClaim(CalculateWriteOffInDTO request)
		{
			return await _claimCal.CalculateClaim(request);
		}

		public IEnumerable<Assessor> GetAssessorList()
		{
			return _assessorRepository.ListAll();
		}

		public bool UpdateAuditTrail(string claimNumber, string comments)
		{
			return _auditTrailRepository.UpdateComment(claimNumber, comments);
		}

	}
}
