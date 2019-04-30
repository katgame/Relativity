using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Entities;
using relativityCalculator.Core.Models;

namespace relativityCalculator.Core.Contracts
{
	public interface IRepository<T> where T : BaseEntity
	{
		T GetById(int id);
		T GetByEmail(string email);
		T GetSingleBySpec(ISpecification<T> spec);
		IEnumerable<T> ListAll();
		IEnumerable<T> List(ISpecification<T> spec);
		T Add(T entity);
		AuditLog Update(T entity);
		void Delete(T entity);

	}
}
