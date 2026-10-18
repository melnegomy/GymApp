
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GymApp.BLL.ViewModels.Session;

public class CreateSessionViewModel
{
    
    [StringLength (200,MinimumLength =10,ErrorMessage = "Description must be between 10 and 200 characters long.")]
    public string Description { get; set; }=default!;
    [Range(1, 30, ErrorMessage = "Capacity must be between 1 and 30.")]
    public int Capacity { get; set; } = default!;
    [Display(Name = "Start Time & Time")]
    public DateTime StartDate { get; set; } = default!;
    [Display(Name = "End Time & Time")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Trainer ")]
    public int TrainerId { get; set; } = default!;
    [Display(Name = "Category ")]
    public int CategoryId { get; set; } = default!;
}
