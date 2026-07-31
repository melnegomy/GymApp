namespace GymApp.DAL.Models;

public class Member : GymUser
{
    public string photo { get; set; } = default!; // URL or path to the member's photo

    // joining date of the member from CreatedAt property of BaseEntity

    public HealthRecord HealthRecord { get; set; } = default!;

    public ICollection<MemberShip> MemberPlans { get; set; } = []; 
    public ICollection<Booking> MemberSessions { get; set; } = [];
}
