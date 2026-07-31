namespace GymApp.DAL.Repositories;

public class GenericRepository<TEntity> 
    : IGenericRepository<TEntity> where TEntity 
    : BaseEntity<int>
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
    public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Add(entity);
      return  await _context.SaveChangesAsync(cancellationToken);
    }
    //Update 
    public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Update(entity);
       return await _context.SaveChangesAsync(cancellationToken);
    }
    //Delete 
    public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Remove(entity);
        return  await _context.SaveChangesAsync(cancellationToken);
    }
}
