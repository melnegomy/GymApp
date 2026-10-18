using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Contracts;

public  interface ISessionRepository : IGenericRepository<Session>
{
    Task< IEnumerable<Session> > GetAllSessionsWithCategoryAndTrainerAsync(CancellationToken cancellationToken = default);
    Task<int> CountOfBookedSlotsAsync(int id, CancellationToken cancellationToken = default);

    Task<Session?> GetWithCategoryAndTrainerByIdAsync(int id, CancellationToken cancellationToken = default);
}
