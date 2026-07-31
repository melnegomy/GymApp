using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Contracts;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity<int>
{
    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, bool trackChanges = false, CancellationToken cancellationToken = default);
}
