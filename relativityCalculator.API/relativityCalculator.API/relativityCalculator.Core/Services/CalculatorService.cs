using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Exceptions;
using relativityCalculator.Core.Models;
using System.Linq;

namespace relativityCalculator.Core.Services
{
    public class CalculatorService : ICalculatorService
    {
		private IAssessorRepository _assessorRepository;
		private IRelativityLookUpRepository _relativityLookUpRepository;
		private IRelativityRepository _relativityRepository;
		private IAreaRepository _areaRepository;
		private IDbHandler _idbhandler;
		private IAuditTrailRepository _auditTrailRepository;
		internal ValidateClaim _claimCal;

        public CalculatorService(IDbHandler dbHandler,
			IAssessorRepository assessorRepository,
			IRelativityLookUpRepository relativityLookUpRepository,
			IAreaRepository areaRepository,
			IAuditTrailRepository auditTrailRepository,
			IRelativityRepository relativityRepository)
        {
            _idbhandler = dbHandler;
			_assessorRepository = assessorRepository;
			_relativityLookUpRepository = relativityLookUpRepository;
			_areaRepository = areaRepository;
			_auditTrailRepository = auditTrailRepository;
			_relativityRepository = relativityRepository;
			_claimCal = new ValidateClaim(relativityLookUpRepository,
				_areaRepository, 
				_assessorRepository,
				_auditTrailRepository,
				relativityRepository);
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
			 _assessorRepository.Update(assessor, assessor.Id.Value);
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

		public async Task<IList<Relativity>> GetRelativityGroup()
		{
			return await _relativityRepository.ListAllAsync();
		}

		public async Task<IList<RelativityLookUp>> GetRelativityByGroupId(string relativityId)
		{
			return _relativityLookUpRepository.ListAll().Where(x => x.RelativityId == relativityId).ToList();
		}
	}
}
