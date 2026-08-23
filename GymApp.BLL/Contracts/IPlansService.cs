using GymApp.DAL.Models;

namespace GymApp.BLL.Contracts
{
    public interface IPlansService
    {
        Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Plan>> PlansAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(Plan plan, CancellationToken cancellationToken);
        Task<bool> IsActiveAsync(int id, CancellationToken cancellationToken);
    }
}