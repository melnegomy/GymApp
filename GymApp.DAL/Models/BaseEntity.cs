namespace GymApp.DAL.Models;
public abstract class BaseEntity<key>
{
    public key Id { get; set; }
    public DateTime? CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
}
