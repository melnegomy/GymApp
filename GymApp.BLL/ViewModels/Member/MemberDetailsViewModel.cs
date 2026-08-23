namespace GymApp.BLL.ViewModels.Member;

public class MemberDetailsViewModel
{
    public int Id { get; set; }
    public string? Photo { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string DateOfBirth { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string MemberShipStartDate { get; set; } = default!;
    public string MemberShipEndDate { get; set; } = default!;
    public string PlanName { get; set; } = default!;
}
