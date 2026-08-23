using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.ViewModels.Trainer;

public class TrainerEditViewModel
{
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

    [Required(ErrorMessage = "Specialization must be picked from the predefined dropdown list")]
    public string? Specialties { get; set; } = default!;
}
