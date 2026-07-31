using GymApp.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymApp.DAL.Models;

public class GymUser : BaseEntity<int>
{
    
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }   

    public Gender Gender { get; set; }
    public Address Address { get; set; }
}
[Owned]
public class Address 
{
    public int BuildingNumber { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    
}