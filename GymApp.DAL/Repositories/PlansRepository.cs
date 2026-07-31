using GymApp.DAL.Context;
using GymApp.DAL.Contracts;
using GymApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace GymApp.DAL.Repositories;



public class PlansRepository : GenericRepository<Plan>,IGenericRepository<Plan>
{
    public PlansRepository(GymDbContext context): base(context)
    {

    }
    

}
