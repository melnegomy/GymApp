using System.ComponentModel.DataAnnotations;

namespace GymApp.BLL.ViewModels.Member;

public class HealthRecordViewModel
{
    [Range(0, 300, ErrorMessage ="Height must be between 0 and 300")]
    public decimal Height { get; set; }
    [Range(0, 500, ErrorMessage = "Weight must be between 0 and 300")]

    public decimal Weight { get; set; }
    [Required(ErrorMessage = "BloodType is required")]
    [StringLength(3, ErrorMessage = "Blood type must be exactly  3 char")]

    public string BloodType { get; set; } = default!;
    [StringLength(250, ErrorMessage = "Note must be between 0 and 250")]

    public string? Note { get; set; } = default!;

}
