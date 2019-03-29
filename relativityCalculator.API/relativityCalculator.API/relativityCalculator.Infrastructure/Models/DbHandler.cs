using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace relativityCalculator.Infrastructure.Models
{
    public class DbHandler : IDbHandler
    {

		private readonly string _connectionString;
		private string _exception;
		private readonly IOptions<AppSettings> _config;
		

		public DbHandler(IOptions<AppSettings> config)
		{
			_config = config;
			_connectionString = config.Value.ConnectionString;
			
		}

	
		public PolicyDetailsOutDTO GetPolicyVehicleDetails(string policyNumber)
		{
			var result = new PolicyDetailsOutDTO();

			try
			{
				//1. Create the command object
				var commandNew = new OracleCommand
				{
					CommandText = "Get_Policy_MotorDetails",
					CommandType = CommandType.StoredProcedure
				};

				//2. Set the command parameters
				commandNew.Parameters.Add(new OracleParameter("policyNumber", policyNumber));


				commandNew.Parameters.Add("curreturn", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

				//3. Create a connection object

				using (var connew = new OracleConnection(_connectionString))
				{
					try
					{
						commandNew.Connection = connew;
						//4. Open the connection and execute the command
						connew.Open();
						var drNew = commandNew.ExecuteReader();
						result.policyNumber = policyNumber;
						result.vehicleDetail = new List<VehicleDetail>();
						if (!drNew.HasRows)
						{
							result.bSuccess = false;
							result.Errors = "No Vehicles found for this policy number";
							return result;
						}
						// output the results and close the connection.
						while (drNew.Read())
						{
							result.vehicleDetail.Add(new VehicleDetail
							{
								multiQuoteNumber = drNew["multi_item_key"].ToString(),
								region = drNew["mul_risk_add3"].ToString(),
								vehicleDescription = drNew["mul_display"].ToString(),
								vehicleMake = drNew["mmo_make"].ToString(),
								vehicleMass = drNew["mmo_mass"].ToString(),
								vehicleModel = drNew["mmo_model"].ToString().Substring(0, drNew["mmo_model"].ToString().IndexOf(' ')),
								vehicleSumInsured = drNew["mul_sum_insured"].ToString(),
								vehicleYear = drNew["mmo_manufac_year"].ToString(),
								vehicleRegistration = drNew["mmo_registration"].ToString(),
								postalCode = drNew["mul_post_code"].ToString()

							});
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						//5. Do clean up
						if (connew.State == ConnectionState.Open)
							connew.Close();
					}
					result.bSuccess = true;
					return result;
				}
				;
			}
			catch (Exception ex)
			{
				result.Errors = ex.StackTrace;
				result.bSuccess = false;
				return result;
			}
		}

	}
}