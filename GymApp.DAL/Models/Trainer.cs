using GymApp.DAL.Models.Enums;

namespace GymApp.DAL.Models;

public class Trainer : GymUser
{
    public Specialties Specialty { get; set; }
    // Hire date of the trainer from CreatedAt property of BaseEntity

    public ICollection<Session> Sessions { get; set; } =[];
}
