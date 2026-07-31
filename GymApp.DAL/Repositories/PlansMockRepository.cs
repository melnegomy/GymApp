using GymApp.DAL.Contracts;
using GymApp.DAL.Models;

namespace GymApp.DAL.Repositories
{
    public class PlansMockRepository // : IPlansRepository
    {
        private static readonly List<Plan> _plans = new List<Plan>
            {
                new Plan { Id = 1, Name = "Basic Plan", Description = "Access to gym equipment and facilities.", Price = 29.99m },
                new Plan { Id = 2, Name = "Standard Plan", Description = "Includes Basic Plan benefits plus group classes.", Price = 49.99m },
                new Plan { Id = 3, Name = "Premium Plan", Description = "Includes Standard Plan benefits plus personal training sessions.", Price = 79.99m }
            };

        public async Task<IEnumerable<Plan>> GetAllAsync(bool trackChanges=false, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_plans.AsEnumerable());
        }
        public async Task<Plan?> GetByIdAsync(int id , bool trackChanges=false, CancellationToken cancellationToken = default)
        {
            var plan = _plans.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(plan);
        }

       
    }
}
