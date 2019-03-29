using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Enums;
namespace relativityCalculator.Core.Models
{
	public class ValidateClaim
	{
		private IRelativityRepository _relativityRepository;
		private IRelativityConfig _relativityConfig; 
		private IAreaRepository _areaRepository;
		private IAssessorRepository _assessorRepository;
		private IAuditTrailRepository _auditTrailRepository;
		private List<Relativity> KeysList;
		private string NullRelativityError = "Could not find relativity value for ";
		private StringBuilder ErrorResponseMessage;
		public ValidateClaim(IRelativityRepository relativityRepository,
			IAreaRepository areaRepository,
			IRelativityConfig relativityConfig,
			IAssessorRepository assessorRepository,
			IAuditTrailRepository auditTrailRepository)
		{
			_relativityRepository = relativityRepository;
			_areaRepository = areaRepository;
			_relativityConfig = relativityConfig;
			_assessorRepository = assessorRepository;
			_auditTrailRepository = auditTrailRepository;
			KeysList = new List<Relativity>();
		}

		public int CalculateVehicleAge(int vehicleYear)
		{
			return DateTime.Now.Year - vehicleYear;
		}

		internal string GetArea(string postalCode)
		{
			 return _areaRepository.GetArea(postalCode);
	    }

		private string GetRelativityKey(int value, string RelativityName)
		{
			IList<RelativityLookUp> massRelativity = _relativityRepository.GetRelativityByName(RelativityName);
			int MinValue, MaxValue;
			foreach (var item in massRelativity)
			{
				if (item.RelativityKey == "Unknown") return "Unknown";

				var originalValue = item.RelativityKey;
				MinValue = int.Parse(item.RelativityKey.Substring(0, item.RelativityKey.IndexOf('<')));
				int lastIndex = item.RelativityKey.IndexOf("=");
				var temp = item.RelativityKey.Remove(0, lastIndex + 1);
				MaxValue = int.Parse(temp);

				if ((value <= MinValue) && (value < MaxValue))
					return originalValue;
			}
			return "Unknown";
		}

		public bool validateRelativities(VehicleDetail request)
		{
			
			var activeRelaticities = _relativityRepository.GetActiveRelativities();

			//UI Inputs
			KeysList.Add(new Relativity { RelativityName = "Make", RequestValue = request.vehicleMake } );
			KeysList.Add(new Relativity { RelativityName = "Model", RequestValue = request.vehicleModel });
			KeysList.Add(new Relativity { RelativityName = "mass_band", RequestValue = GetRelativityKey(int.Parse(request.vehicleMass), "mass_band") });
			KeysList.Add(new Relativity { RelativityName = "Vehicle_age", RequestValue = CalculateVehicleAge(int.Parse(request.vehicleYear)).ToString() });
			KeysList.Add(new Relativity { RelativityName = "Year", RequestValue = request.incidentYear });
			KeysList.Add(new Relativity { RelativityName = "Area", RequestValue = GetArea(request.region) });
			KeysList.Add(new Relativity { RelativityName = "SI_Band", RequestValue = GetRelativityKey(int.Parse(request.vehicleSumInsured), "SI_Band") });

			var relativityValues = _relativityRepository.GetRelativityByKey(KeysList);

			if (!areRelativityValueValid(relativityValues))
				return false;

			return true;
		}

		public async Task<CalculateWriteOffOutDTO> CalculateClaim(CalculateWriteOffInDTO request)
		{
			var response = new CalculateWriteOffOutDTO();
			var Cost = new Costs();
			try
		{	
			if(validateRelativities(request.vehicleDetail))
			{
					//3.	Cost of salvage = 3 507.50
				Cost.CostOfSalvage = _relativityConfig.GetConfigValue("CostOfSalvalge");
				Cost.CompanyPercentage = _assessorRepository.GetCompanyPercentage(int.Parse(request.assessorId)) ;
				Cost.additionalCost = (Cost.CompanyPercentage / 100) * int.Parse(request.vehicleDetail.repairCost);

					//2.	Total cost of repair = Cost of repair (incl. VAT) + Additional costs
				Cost.TotalRepairCost = int.Parse(request.vehicleDetail.repairCost) + Cost.additionalCost;
				Cost.BasePrice = _relativityConfig.GetConfigValue("Base");

					//4.	Expected Salvage Recovery = Base * Incident year relativity * Make relativity * Model relativity *
					//Age relativity * Sum insure relativity * Mass relativity * Area relativity *
					//Locks and keys relativity * Engine start relativity
				Cost.ExpectedSalvageRecovery = Convert.ToDouble(Cost.BasePrice);
				foreach (var item in KeysList)
				{
						Cost.ExpectedSalvageRecovery *= item.RelativityValue.Value;
				}
				Cost.ExpectedSalvageRecovery = Math.Round(Cost.ExpectedSalvageRecovery, 5);

					//6.	Total Salvage Recovery = Expected salvage recovery - Cost of salvage  
				Cost.TotalSalvageRecovery = Math.Round(Cost.ExpectedSalvageRecovery - Convert.ToDouble(Cost.CostOfSalvage),4);

					//7.	Saving = |Sum insured(“market value”) – Total salvage recovery – Total cost of repair|. This must be the absolute value.
				Cost.Saving = Math.Round(Convert.ToDouble(request.vehicleDetail.vehicleSumInsured)  - (Cost.TotalSalvageRecovery - Cost.TotalRepairCost), 0);

				response.recommendation = GetRecommendation(Cost.Saving, Cost.TotalSalvageRecovery,
					Cost.TotalRepairCost, double.Parse(request.vehicleDetail.vehicleSumInsured)).ToString();

				//Log details
				response.auditTrailId = LogCalculationResult(request, Cost, response.recommendation).ToString();
				response.bSuccess = true;
				return response;
			}
			else
			{
				response.Errors = ErrorResponseMessage.ToString();
				response.bSuccess = false;
				return response;
			}
			}
			catch (Exception exception)
			{
				response.Errors = exception.Message;
				response.bSuccess = false;
				return response;
			}
		}

		internal int LogCalculationResult(CalculateWriteOffInDTO request, Costs costs, string recommendation)
		{
			var LogDetails = new AuditTrail()
			{
				AdditionalCosts = costs.additionalCost.ToString(),
				Area = request.vehicleDetail.region,
				ClaimNumer = request.claimNumber,
				CostOfSalvage = costs.CostOfSalvage.ToString(),
				ExpectedSalvageRecovery = costs.ExpectedSalvageRecovery.ToString(),
				IncidentYear = request.vehicleDetail.incidentYear,
				Recommendation = recommendation,
				TotalCostRepair = costs.TotalRepairCost.ToString(),
				RepairCost = request.vehicleDetail.repairCost,
				TotalSalvageRecovery = costs.TotalSalvageRecovery.ToString(),
				VehicleMake = request.vehicleDetail.vehicleMake,
				VehicleMass = request.vehicleDetail.vehicleMass,
				VehicleModel = request.vehicleDetail.vehicleModel,
				VehicleSumInsured = request.vehicleDetail.vehicleSumInsured,
				VehicleYear = request.vehicleDetail.vehicleYear,
				DifferenceInCost = costs.Saving.ToString(),
				Comments = string.Empty
			};

			var auditTrailId = _auditTrailRepository.Add(LogDetails);
			return auditTrailId.Id;
		}

		internal Recommendation GetRecommendation(double saving, double TotalSalvageRecovery, double TotalRepairCost, double sumInsured)
		{
			var percentageValue = 0.1;
			//If Sum insured – Total salvage recovery > Total cost of repair then Recommendation
			//= Repair (this should be highlighted in green with sub line reading “Repair vehicle”)
			if ((sumInsured - TotalSalvageRecovery) > TotalRepairCost)
				return Recommendation.Repair;

			/*92
			 * If Sum insured – Total salvage recovery <= Total cost of repair and Saving/(Total cost of repair)
			 * <= 10% then Recommendation = Write-off (this should be highlighted in orange with sub line reading
			 * “Room for negotiation on repair cost to prevent write-off”)
			 */
			else if (((sumInsured - TotalSalvageRecovery) <= TotalRepairCost) && ((saving / TotalRepairCost) <= percentageValue))
				return Recommendation.Negotiate;
			
			/*
			If Sum insured – Total salvage recovery <= Total cost of repair and Saving/(Total cost of repair) >
			10% then Recommendation = Write-off (this should be highlighted in red with sub line reading “Write-off vehicle”))
			 */
			else if (((sumInsured - TotalSalvageRecovery) <= TotalRepairCost) && ((saving / TotalRepairCost) > percentageValue))
				return Recommendation.Write_Off;

			else return Recommendation.InValid;

		}

		private bool areRelativityValueValid(IList<Relativity> relativityList)
		{
			foreach (var item in relativityList)	
			{
				if (double.IsNaN(item.RelativityValue.Value) || item.RelativityValue.Value == 0)
				{
					NullRelativityError +=  "Vehicle " + item.RelativityName;
					ErrorResponseMessage = new StringBuilder(NullRelativityError);
					ErrorResponseMessage.Insert(NullRelativityError.Length, " : " + item.RequestValue);
					return false;
				}
			}
			return true;
		}
	}
}
