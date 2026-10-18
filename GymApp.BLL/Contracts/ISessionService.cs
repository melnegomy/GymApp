using GymApp.BLL.Common;
using GymApp.BLL.ViewModels.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.Contracts;

public interface ISessionService
{
    public Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken cancellationToken = default);

    Task <Result> CreateSessionAsync( CreateSessionViewModel createSessionViewModel ,CancellationToken cancellationToken = default); 
    Task<IEnumerable<CategorySelectItemViewModel>> GetCategorySelectItemAsync(CancellationToken cancellationToken = default);
    Task<Result<SessionViewModel>> GetSessionByIdAsync(int sessionId, CancellationToken cancellationToken = default);
    Task<Result<UpdateSessionViewModel>> GetSessionToUpdateAsync(int sessionId,CancellationToken cancellationToken = default);
    Task<Result> UpdateSessionAsync(int sessionId, UpdateSessionViewModel updateSessionViewModel, CancellationToken cancellationToken = default);
    Task<Result> DeleteSessionAsync(int id, CancellationToken cancellationToken);
}
