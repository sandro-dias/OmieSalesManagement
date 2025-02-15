using Application.Data.Repository;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Domain.Shared;
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
            return await specificationResult.FirstOrDefaultAsync(cancellationToken)!;
        }

        public virtual async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await DbContext.Set<T>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().ToListAsync(cancellationToken);
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

        public Task DeleteListAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            DbContext.Entry(entities).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public async Task<PagedResult<T>> GetPagedResultAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var totalCount = await DbContext.Set<T>().CountAsync(cancellationToken);
            var items = await DbContext.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }
    }
}
