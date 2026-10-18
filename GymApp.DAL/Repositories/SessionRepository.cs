using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Repositories;

public class SessionRepository : GenericRepository<Session>, ISessionRepository
{
    public SessionRepository(GymDbContext context) : base(context)
    {
    }

    public async Task<int> CountOfBookedSlotsAsync(int SessionId, CancellationToken cancellationToken = default)
        => await _context.Bookings.CountAsync(X=>X.SessionId == SessionId);

    public async Task<IEnumerable<Session>> GetAllSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default)
        => await _context.Sessions.AsNoTracking()
            .Include(s => s.Category)
            .Include(s => s.Trainer)
            .ToListAsync(cancellationToken);

    public async Task<Session?> GetWithCategoryAndTrainerByIdAsync(int id, CancellationToken cancellationToken = default)
    => await _context.Sessions.AsNoTracking()
            .Include(s => s.Category)
            .Include(s => s.Trainer)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
}
