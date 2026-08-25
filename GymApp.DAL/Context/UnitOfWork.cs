using GymApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly GymDbContext _dbcontext;
    private readonly Dictionary<string, Object> _repositories = [];

    public UnitOfWork(GymDbContext context, ISessionRepository session)
    {
        _dbcontext = context;
        Session = session;
    }

    public ISessionRepository Session { get; }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;

        if (_repositories.TryGetValue(entityName, out Object? value))
            return (IGenericRepository<TEntity>)value;
        

        var repo = new GenericRepository<TEntity>(_dbcontext);
        _repositories.Add(entityName, repo);   
        return repo;

    }  

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbcontext.SaveChangesAsync(cancellationToken);
}
