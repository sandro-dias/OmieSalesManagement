﻿using Ardalis.Specification;
using Domain.Entities;

namespace Application.Data.Repository
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
