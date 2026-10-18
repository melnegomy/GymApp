using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Session;

public class UpdateSessionViewModel
{

    [StringLength(200, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters long.")]
    public string Description { get; set; } = default!;
    
    [Display(Name = "Start Time & Time")]
    public DateTime StartDate { get; set; } = default!;
    [Display(Name = "End Time & Time")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Trainer ")]
    public int TrainerId { get; set; } = default!;
   
}