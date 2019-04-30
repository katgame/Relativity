using System;
using System.Collections.Generic;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public class Assessor : BaseEntity, IAggregateRoot
	{
        public string CompanyName { get; set; }
        public string CompanyRegNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkNumber { get; set; }
        public string CellNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string DeviceUsed { get; set; }
		public int? CompanyTypeId { get; set; }

	}
}
