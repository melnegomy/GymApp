using GymApp.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace GymApp.BLL.ViewModels.Member;

public class MemberEditViewModel
{
    public string? Photo { get; set; } = default!;
    public string? Name { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Phone is required")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = default!;

    [Required(ErrorMessage = "BuildingNumber is required")]
    public int BuildingNumber { get; set; }
    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; } = default!;
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = default!; 
}
