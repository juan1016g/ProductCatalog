using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> IsNameUniqueAsync(string name);
    }
}
