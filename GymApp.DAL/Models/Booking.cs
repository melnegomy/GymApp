namespace GymApp.DAL.Models;

public class Booking : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = default!;
    public int SessionId { get; set; }
    public Session Session { get; set; } = default!;

    public bool Isattended { get; set; }
    //booking day => from base entity CreatedAt property

}
