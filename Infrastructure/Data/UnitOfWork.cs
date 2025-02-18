using Application.Data;
using Application.Data.Repository;
using Infrastructure.Data.Repository;

namespace Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SalesManagementContext _dbContext;
        public ISalesRepository _salesRepository;
        public IProductRepository _productRepository;
        public ISalesmanRepository _salesmanRepository;

        public UnitOfWork(SalesManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ISalesRepository SalesRepository
        {
            get { return _salesRepository ??= new SalesRepository(_dbContext); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_dbContext); }
        }

        public ISalesmanRepository SalesmanRepository
        {
            get { return _salesmanRepository ??= new SalesmanRepository(_dbContext); }
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
