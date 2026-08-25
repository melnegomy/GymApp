using GymApp.BLL.Contracts;
using GymApp.DAL.Contracts;
using GymApp.DAL.Models;
using GymApp.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace GymApp.BLL.Services;

public class PlansService : IPlansService
{

    private readonly IUnitOfWork _unitOfWork;

    public PlansService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    //PlansAsync gets all plans
    public async Task<IEnumerable<Plan>> PlansAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.GetRepository<Plan>().GetAllAsync(trackChanges: false, cancellationToken: cancellationToken);
    }
    // GetByIdAsync gets a plan by id
    public async Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.GetRepository<Plan>().GetByIdAsync(id, trackChanges: false, cancellationToken: cancellationToken);
    }

    public Task UpdateAsync(Plan plan, CancellationToken cancellationToken)
    {
        _unitOfWork.GetRepository<Plan>().Update(plan, cancellationToken);
        return _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> IsActiveAsync(int id, CancellationToken cancellationToken)
    {
        var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(id, trackChanges: true, cancellationToken: cancellationToken);

        if (plan == null)
            return false;

        plan.IsActive = !plan.IsActive;

         _unitOfWork.GetRepository<Plan>().Update(plan, cancellationToken);
         await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        return true;
    }
}
