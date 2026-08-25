using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL.ViewModels.Session;

public class SessionViewModel
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Duration => EndDate - StartDate;
    public int AvailableSlots { get; set; }
    public string TrainerName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;

    public string Status
    {
        get
        {
            var currentTime = DateTime.Now;
            if (StartDate <= currentTime && EndDate > currentTime)
            {
                return "Ongoing";
            }
            else if (EndDate < currentTime)
            {
                return "Completed";
            }
            else
            {
                return "Upcoming";
            }
        }
    }
}
