﻿using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Core.Models;
using relativityCalculator.Core.Entities;

namespace relativityCalculator.Infrastructure.Models
{
	public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
	{
		protected readonly RelativitiesContext _dbContext;
		public static AuditLog _auditLog;

		public EfRepository(RelativitiesContext dbContext)
		{
			_dbContext = dbContext;
		}


		public virtual T GetById(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public T GetByEmail(string email)
		{
			return _dbContext.Set<T>().Find(email);
		}


		public T GetSingleBySpec(ISpecification<T> spec)
		{
			return List(spec).FirstOrDefault();
		}


		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public IEnumerable<T> ListAll()
		{
			return _dbContext.Set<T>().AsEnumerable();
		}

		public async Task<List<T>> ListAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public IEnumerable<T> List(ISpecification<T> spec)
		{
			// fetch a Queryable that includes all expression-based includes
			var queryableResultWithIncludes = spec.Includes
				.Aggregate(_dbContext.Set<T>().AsQueryable(),
					(current, include) => current.Include(include));

			// modify the IQueryable to include any string-based include statements
			var secondaryResult = spec.IncludeStrings
				.Aggregate(queryableResultWithIncludes,
					(current, include) => current.Include(include));

			// return the result of the query using the specification's criteria expression
			return secondaryResult
							.Where(spec.Criteria)
							.AsEnumerable();
		}
		public async Task<List<T>> ListAsync(ISpecification<T> spec)
		{
			// fetch a Queryable that includes all expression-based includes
			var queryableResultWithIncludes = spec.Includes
				.Aggregate(_dbContext.Set<T>().AsQueryable(),
					(current, include) => current.Include(include));

			// modify the IQueryable to include any string-based include statements
			var secondaryResult = spec.IncludeStrings
				.Aggregate(queryableResultWithIncludes,
					(current, include) => current.Include(include));

			// return the result of the query using the specification's criteria expression
			return await secondaryResult
							.Where(spec.Criteria)
							.ToListAsync();
		}

		public T Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();

			return entity;
		}

		public async Task<T> AddAsync(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public AuditLog Update(T entity)
		{
			try
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				_dbContext.SaveChanges();
				return EfRepository<AuditLog>._auditLog;
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}
		public async Task UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			_dbContext.SaveChanges();
		}
		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}


	}
}
