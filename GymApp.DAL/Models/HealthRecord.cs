using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Models;

public class HealthRecord : BaseEntity
{
    public decimal height { get; set; }
    public decimal weight { get; set; } = 0;    
    public string bloodType { get; set; } = default!;
    public string? Note { get; set; }

    // Last updated date From BaseEntity UpdatedAt property

    public int MemberId { get; set; }
    public Member Member { get; set; } = default!;
}
