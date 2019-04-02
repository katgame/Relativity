using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;
using System.Linq;

namespace relativityCalculator.API.Controllers
{
	[Produces("application/json")]
	public class RelativityController : Controller
    {
		internal ICalculatorService _calculatorService { get; }

		public RelativityController(ICalculatorService calculatorService)
		{
			_calculatorService = calculatorService;
		}

		/// <summary>
		/// Get Policy Details
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/getPolicyDetails")]
		[EnableCors("CorsPolicy")]
		[HttpGet]
		public async Task<IActionResult> GetPolicyDetails(string policyNumber)
		{
			PolicyDetailsOutDTO result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = _calculatorService.GetPolicyVehicleDetails(policyNumber);
				if (result.bSuccess == false)
					return BadRequest(result);

			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action get policy details" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Add New Assessor 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/addAssessor")]
		[EnableCors("CorsPolicy")]
		[HttpPost]
		public async Task<IActionResult> AddAssessor([FromBody] Assessor request)
		{
			bool result;
			try
			{
				if (!ModelState.IsValid)
				{
					//var message = string.Join(" | ", ModelState.Values
				 // .SelectMany(v => v.Errors)
				 // .Select(e => e.ErrorMessage));
					return BadRequest(ModelState);
					//return BadRequest(message);
				}

				_calculatorService.AddAssessor(request);
				result = true;
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action add assessor" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Update Assessor
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/updateAssessor")]
		[EnableCors("CorsPolicy")]
		[HttpPost]
		public async Task<IActionResult> UpdateAssessor([FromBody] Assessor request)
		{
			bool result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				_calculatorService.UpdateAssessor(request);
				result = true;
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action update assessor" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Update Assessor
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/deleteAssessor")]
		[EnableCors("CorsPolicy")]
		[HttpPost]
		public async Task<IActionResult> DeleteAssessor([FromBody] Assessor request)
		{
			bool result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				_calculatorService.UpdateAssessor(request);
				result = true;
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action update assessor" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Get Assessor details
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/getAssessorList")]
		[EnableCors("CorsPolicy")]
		[HttpGet]
		public async Task<IActionResult> GetAssessor()
		{
			IEnumerable<Assessor> result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = _calculatorService.GetAssessorList();
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action get assessor list" + ex);
			}
			return new OkObjectResult(result);
		}


		/// <summary>
		/// Calculate Claim and get recemmendation
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/calculateVehicle")]
		[EnableCors("CorsPolicy")]
		[HttpPost]
		public async Task<IActionResult> CalculateClaim([FromBody] CalculateWriteOffInDTO request)
		{
			CalculateWriteOffOutDTO result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = await _calculatorService.CalculateClaim(request);
				if (result.bSuccess == false)
					return BadRequest(result);
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action calculate claim" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Update Claim Trail
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/updatecomment")]
		[EnableCors("CorsPolicy")]
		[HttpPut]
		public async Task<IActionResult> UpdateComment([FromBody] AuditTrailInDTO request)
		{
			bool result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = _calculatorService.UpdateAuditTrail(request.auditTrailId, request.comment);

			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action update comment" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Get Relativities
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/getrelativitygroup")]
		[EnableCors("CorsPolicy")]
		[HttpGet]
		public async Task<IActionResult> GetRelativityGroup()
		{
			IList<Relativity> result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = await _calculatorService.GetRelativityGroup();

			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action get relativities" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Get Relativities
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/getrelativitybygroup")]
		[EnableCors("CorsPolicy")]
		[HttpGet]
		public async Task<IActionResult> GetRelativityByGroupName(string relativityId)
		{
			IList<RelativityLookUp> result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = await _calculatorService.GetRelativityByGroupId(relativityId);
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action get relativities" + ex);
			}
			return new OkObjectResult(result);
		}
	}
}