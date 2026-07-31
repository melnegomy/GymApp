using GymApp.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Member;

public class CreateMemberViewModel
{
    // Member Properties

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression ("^[a-zA-Z]+$",ErrorMessage ="Name can only contain letters and spaces")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress(ErrorMessage ="Invalid email format")]
    //[DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Phone is required")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = default!;

    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    // Address Properties
    [Required(ErrorMessage = "BuildingNumber is required")]
    public int BuildingNumber { get; set; }
    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; } = default!;
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = default!;
    [Required(ErrorMessage = "HealthRecord is required")]

    public HealthRecordViewModel HealthRecord {  get; set; } = default!; 
}
