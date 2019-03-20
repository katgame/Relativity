﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using relativityCalculator.Core.DTOs;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Contracts;

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
		/// Add Assessor
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
					return BadRequest(ModelState);
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
		/// Get Assessor
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/getAssessorList")]
		[EnableCors("CorsPolicy")]
		[HttpPost]
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
		/// Get Assessor
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Route("api/calculateVehicle")]
		[EnableCors("CorsPolicy")]
		[HttpPost]
		public async Task<IActionResult> CalculateClaim([FromBody] VehicleDetail request)
		{
			CalculateWriteOffOutDTO result;
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				result = await _calculatorService.CalculateClaim(request);
			}
			catch (Exception ex)
			{
				return BadRequest("Error in a action calculate claim" + ex);
			}
			return new OkObjectResult(result);
		}

		/// <summary>
		/// Import Look Up sheet
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		//[ProducesResponseType(200)]
		//[ProducesResponseType(400)]
		//[ProducesResponseType(500)]
		//[Route("api/importLookUp")]
		//[EnableCors("CorsPolicy")]
		//[HttpPost]
		//public async Task<IActionResult> ImportFile([FromBody] VehicleDetail request)
		//{
		//	CalculateWriteOffOutDTO result;
		//	try
		//	{
		//		if (!ModelState.IsValid)
		//		{
		//			return BadRequest(ModelState);
		//		}

		//		result = await _calculatorService.CalculateClaim(request);
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest("Error in a action calculate claim" + ex);
		//	}
		//	return new OkObjectResult(result);
		//}
	}
}