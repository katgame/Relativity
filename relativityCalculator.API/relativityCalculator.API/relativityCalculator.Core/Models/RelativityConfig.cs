using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Core.Models
{
    public partial class RelativityConfig : BaseEntity, IAggregateRoot
	{
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyGroup { get; set; }
    }
}
