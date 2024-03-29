﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Text;
using relativityCalculator.Core.Entities;


namespace relativityCalculator.Core.Contracts
{
	public interface IAsyncRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<List<T>> ListAllAsync();
		Task<List<T>> ListAsync(ISpecification<T> spec);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
