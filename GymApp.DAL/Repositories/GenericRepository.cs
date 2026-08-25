using System.Linq.Expressions;

namespace GymApp.DAL.Repositories;

public class GenericRepository<TEntity> 
    : IGenericRepository<TEntity> where TEntity 
    : BaseEntity
{
    protected readonly GymDbContext _context;

    public GenericRepository(GymDbContext context)
    {
        _context = context;
    }
    //GET ALL 
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
    => trackChanges ? await _context.Set<TEntity>().ToListAsync(cancellationToken)
          : await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    //GET by id
    public async Task<TEntity?> GetByIdAsync(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().FindAsync([id], cancellationToken);
    //Add 
    public void Add(TEntity entity, CancellationToken cancellationToken = default)
       => _context.Set<TEntity>().Add(entity);
    //Update 
    public void Update(TEntity entity, CancellationToken cancellationToken = default) 
        => _context.Set<TEntity>().Update(entity);      
    //Delete 
    public void Delete(TEntity entity, CancellationToken cancellationToken = default)
         => _context.Set<TEntity>().Remove(entity);
        
    

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>();
    }
}
