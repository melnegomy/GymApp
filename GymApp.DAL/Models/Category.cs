namespace GymApp.DAL.Models;

public class Category : BaseEntity<int>
{
    public string Name { get; set; } = default!;

    public ICollection<Session> Sessions { get; set; } =[];
}
