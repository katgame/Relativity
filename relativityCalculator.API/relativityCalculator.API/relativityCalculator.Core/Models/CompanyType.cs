using System;
using System.Collections.Generic;

namespace relativityCalculator.Core.Models
{
    public partial class CompanyType
    {
        public CompanyType()
        {
            Assessor = new HashSet<Assessor>();
        }

        public int Id { get; set; }
        public string CompanyType1 { get; set; }
        public int? Percentage { get; set; }

        public ICollection<Assessor> Assessor { get; set; }
    }
}
