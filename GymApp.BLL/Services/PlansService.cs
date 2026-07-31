using GymApp.BLL.Contracts;
using GymApp.DAL.Contracts;
using GymApp.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
namespace GymApp.BLL.Services;

public class PlansService : IPlansService
{

    private readonly IGenericRepository<Plan> _plansRepository;
    public PlansService(IGenericRepository<Plan> plansRepository) 
    { 
        _plansRepository = plansRepository;
    } 


    //PlansAsync gets all plans
    public async Task<IEnumerable<Plan>> PlansAsync(CancellationToken cancellationToken = default)
    {
        return await _plansRepository.GetAllAsync(trackChanges: false, cancellationToken: cancellationToken);
    }
    // GetByIdAsync gets a plan by id
    public async Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _plansRepository.GetByIdAsync(id, trackChanges: false, cancellationToken: cancellationToken);
    }

}
