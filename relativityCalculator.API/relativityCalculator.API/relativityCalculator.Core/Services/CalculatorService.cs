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
		private IAuditLogRepository _auditLogRepository;
		internal ValidateClaim _claimCal;

        public CalculatorService(IDbHandler dbHandler,
			IAssessorRepository assessorRepository,
			IRelativityLookUpRepository relativityLookUpRepository,
			IAreaRepository areaRepository,
			IAuditTrailRepository auditTrailRepository,
			IAuditLogRepository auditLogRepository,
			IRelativityRepository relativityRepository)
        {
            _idbhandler = dbHandler;
			_assessorRepository = assessorRepository;
			_relativityLookUpRepository = relativityLookUpRepository;
			_areaRepository = areaRepository;
			_auditTrailRepository = auditTrailRepository;
			_relativityRepository = relativityRepository;
			_auditLogRepository = auditLogRepository;
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

		public async Task<IList<Relativity>> GetRelativityGroup()
		{
			return await _relativityRepository.ListAllAsync();
		}

		public async Task<IList<RelativityLookUp>> GetRelativityByGroupId(string relativityId)
		{
			return _relativityLookUpRepository.ListAll().Where(x => x.RelativityId == relativityId).ToList();
		}

		public async Task<IList<AuditTrail>> GetAuditTrail()
		{
			return await _auditTrailRepository.ListAllAsync();
		}

		public void UpdateRelativity(UpdateRelativityInDTO request)
		{
			var NewLogTrail = _relativityLookUpRepository.Update(request.relativity);
			if (NewLogTrail != null)
				UpdateLogReason(NewLogTrail, request.UpdateReason, request.UserId.ToString());
		}

		internal void UpdateLogReason(AuditLog auditLog, string reason, string userId)
		{
			auditLog.UpdateReason = reason;
			auditLog.UserId = userId;
			_auditLogRepository.UpdateAsync(auditLog);
		}
	}
}
