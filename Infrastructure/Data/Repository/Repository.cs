﻿using Application.Data.Repository;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class Repository<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly SalesManagementContext DbContext;

        public Repository(SalesManagementContext dbContext)
        {
            DbContext = dbContext;
        }

        public Repository(){ }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await DbContext.Set<T>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.Default.GetQuery(DbContext.Set<T>().AsQueryable(), spec);
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }
    }
}
