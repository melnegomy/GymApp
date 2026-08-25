using GymApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Contracts;

public interface IUnitOfWork
{
    public ISessionRepository Session {  get;}
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
