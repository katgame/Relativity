using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Contracts;

namespace relativityCalculator.Core.Models
{
	public class ValidateClaim
	{
		private IRelativityRepository _relativityRepository;
		private IAreaRepository _areaRepository;
		private const string NullRelativityError = " ralativity has no value, Please provide a different input value";
		private StringBuilder ErrorResponseMessage;
		public ValidateClaim(IRelativityRepository relativityRepository, IAreaRepository areaRepository)
		{
			_relativityRepository = relativityRepository;
			_areaRepository = areaRepository;
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

		public Tuple<bool,string> validateRelativities(VehicleDetail request)
		{
			var KeysList = new List<Relativity>();
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
				return new Tuple<bool, string>(false, ErrorResponseMessage.ToString());

			//Back end Relativities from Relaticity Config Base price, Compamy Percentage ....


			
			return null;
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
