using System;
using System.Collections.Generic;
using System.Text;

namespace relativityCalculator.Core.Contracts
{
	public interface IAreaRepository
	{
		string GetArea(string postalCode);
	}
}
