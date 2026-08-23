using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Models;

public class MemberShip : BaseEntity
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = default!;
    public int PlanId { get; set; }
    public Plan Plan { get; set; } = default!;

    // Start of the membership From BaseEntity CreatedAt property
    public DateOnly EndDate { get; set; }

    //Is Active => EndDate > Current Date
    [NotMapped]
    public bool IsActive => EndDate > DateOnly.FromDateTime(DateTime.Now);

    //string Status => IsActive ? "Active" : "Inactive"
    [NotMapped]
    public String Status => IsActive ? "Active" : "Inactive";

}
