using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.ViewModels.Trainer;

public class TrainerDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Specialty { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }

    public string Address { get; set; } = default!;
}
