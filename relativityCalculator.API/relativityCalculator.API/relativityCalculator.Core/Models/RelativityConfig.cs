using System;
using System.Collections.Generic;

namespace relativityCalculator.Core.Models
{
    public partial class RelativityConfig
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyGroup { get; set; }
    }
}
