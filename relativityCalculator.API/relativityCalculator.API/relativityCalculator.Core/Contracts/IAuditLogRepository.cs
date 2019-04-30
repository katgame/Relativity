using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{ 
	public interface IAuditLogRepository : IRepository<AuditLog>, IAsyncRepository<AuditLog> {}
}
