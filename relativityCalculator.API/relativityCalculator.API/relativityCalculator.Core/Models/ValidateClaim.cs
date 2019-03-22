using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Contracts;
namespace relativityCalculator.Core.Models
{
	public class ValidateClaim
	{
		private IRelativityRepository _relativityRepository;
		private IRelativityConfig _relativityConfig; 
		private IAreaRepository _areaRepository;
		private IAssessorRepository _assessorRepository;
		private List<Relativity> KeysList;
		private const string NullRelativityError = " ralativity has no value, Please provide a different input value";
		private StringBuilder ErrorResponseMessage;
		public ValidateClaim(IRelativityRepository relativityRepository,
			IAreaRepository areaRepository,
			IRelativityConfig relativityConfig,
			IAssessorRepository assessorRepository)
		{
			_relativityRepository = relativityRepository;
			_areaRepository = areaRepository;
			_relativityConfig = relativityConfig;
			_assessorRepository = assessorRepository;
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
				item.RelativityKey = item.RelativityKey.Remove(0, lastIndex + 1);
				MaxValue = int.Parse(item.RelativityKey);

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

		public CalculateWriteOffOutDTO CalculateClaim(CalculateWriteOffInDTO request)
		{
			if(validateRelativities(request.vehicleDetail))
			{
				//3.	Cost of salvage = 3 507.50
				var CostOfSalvage = _relativityConfig.GetConfigValue("CostOfSalvalge");
				var CompanyPercentage = _assessorRepository.GetCompanyPercentage(int.Parse(request.userId)) ;
				var additionalCost = (CompanyPercentage % 100) * int.Parse(request.vehicleDetail.repairCost);

				//2.	Total cost of repair = Cost of repair (incl. VAT) + Additional costs
				var TotalRepairCost = int.Parse(request.vehicleDetail.repairCost) + additionalCost;
				var BasePrice = _relativityConfig.GetConfigValue("Base");

				//4.	Expected Salvage Recovery = Base * Incident year relativity * Make relativity * Model relativity *
				//Age relativity * Sum insure relativity * Mass relativity * Area relativity *
				//Locks and keys relativity * Engine start relativity

				var ExpectedSalvageRecovery = Convert.ToInt32(BasePrice) * Convert.ToInt32(KeysList.Select(x => x.RelativityValue));

				//6.	Total Salvage Recovery = Expected salvage recovery - Cost of salvage  
				var TotalSalvageRecovery = ExpectedSalvageRecovery - Convert.ToDouble(CostOfSalvage);

				//7.	Saving = |Sum insured(“market value”) – Total salvage recovery – Total cost of repair|. This must be the absolute value.
				var Saving = Convert.ToDouble(request.vehicleDetail.vehicleSumInsured)  - ( TotalSalvageRecovery - TotalRepairCost);


			}
			return null;

		}

		internal string GetRecommendation()
		{
			return string.Empty;

		}



		

		private bool areRelativityValueValid(IList<Relativity> relativityList)
		{
			foreach (var item in relativityList)	
			{
				if (string.IsNullOrEmpty(item.RelativityValue))
				{
					ErrorResponseMessage = new StringBuilder(NullRelativityError);
					ErrorResponseMessage.Insert(0, item.RelativityName);
					return false;
				}
			}
			return true;
		}
	}
}
