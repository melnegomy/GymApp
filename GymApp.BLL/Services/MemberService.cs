using GymApp.DAL.Contracts;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.VisualBasic;

namespace GymApp.BLL.Services;

public class MemberService : IMemberService
{
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<MemberShip> _memberShipRepository;
    private readonly IGenericRepository<Plan> _planRepository;
    private readonly IGenericRepository<HealthRecord> _healthRecordRepository;
    private readonly IGenericRepository<Booking> _bookingRepository;

    public MemberService(IGenericRepository<Member> memberRepository, IGenericRepository<MemberShip> memberShipRepository, IGenericRepository<Plan> planRepository, IGenericRepository<HealthRecord> healthRecordRepository, IGenericRepository<Booking> bookingRepository)
    {
        _memberRepository = memberRepository;
        _memberShipRepository = memberShipRepository;
        _planRepository = planRepository;
        _healthRecordRepository = healthRecordRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken: cancellationToken);

        var result = members.Select(m => new MemberViewModel
        {
            Id = m.Id,
            Name = m.Name,
            Phone = m.Phone,
            Gender = m.Gender.ToString(),
            Photo = m.photo,
            Email = m.Email
        }).ToList();


        return result;
    }

    public async Task<bool> CreateMemberAsync(MemberCreateViewModel createMemberViewModel, CancellationToken cancellationToken = default)
    {
        //var members = await _memberRepository.GetAllAsync(cancellationToken: cancellationToken);
        //var emailExists = members.Any(m=>m.Email == createMemberViewModel.Email);
        //var phoneExists = members.Any(m=>m.Phone == createMemberViewModel.Phone);
        //if (emailExists || phonesExists)
        //    return false;

        var emailExists = await _memberRepository.AnyAsync(m => m.Email == createMemberViewModel.Email, cancellationToken);
        var phoneExists = await _memberRepository.AnyAsync(m => m.Phone == createMemberViewModel.Phone, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        var member = new Member
        {
            Name = createMemberViewModel.Name,
            Email = createMemberViewModel.Email,
            Phone = createMemberViewModel.Phone,
            DateOfBirth = createMemberViewModel.DateOfBirth,
            Gender = createMemberViewModel.Gender,
            photo = "",
            HealthRecord = new HealthRecord
            {
                height = createMemberViewModel.HealthRecord.Height,
                weight = createMemberViewModel.HealthRecord.Weight,
                bloodType = createMemberViewModel.HealthRecord.BloodType,
                Note = createMemberViewModel.HealthRecord.Note,
            },
            Address = new Address
            {
                BuildingNumber = createMemberViewModel.BuildingNumber,
                Street = createMemberViewModel.Street,
                City = createMemberViewModel.City
            }

        };


        return (await _memberRepository.AddAsync(member, cancellationToken: cancellationToken)) > 0;
    }

    public async Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null) return null;
        // Map the member entity to the MemberDetailsViewModel
        var memberDetailsViewModel = new MemberDetailsViewModel
        {
            Id = member.Id,
            Name = member.Name,
            Phone = member.Phone,
            Address = $"{member.Address.BuildingNumber}-{member.Address.Street}, {member.Address.City}",
            DateOfBirth = member.DateOfBirth.ToShortDateString(),
            Email = member.Email,
            Gender = member.Gender.ToString(),
            Photo = "",
        };
        // Get the active membership for the member
        var memberShip = await _memberShipRepository.FirstOrDefaultAsync(m => m.MemberId == member.Id && m.EndDate > DateOnly.FromDateTime(DateTime.Now), cancellationToken: cancellationToken);
        if (memberShip is not null) 
        {
        var plan = await _planRepository.GetByIdAsync(memberShip.PlanId, cancellationToken: cancellationToken);

            memberDetailsViewModel.MemberShipEndDate = memberShip.EndDate.ToShortDateString();
            memberDetailsViewModel.MemberShipStartDate = memberShip.CreatedAt?.ToShortDateString();
            memberDetailsViewModel.PlanName = plan?.Name ?? "No Plan";


        }

        return memberDetailsViewModel;
    }

    public async Task<HealthRecordViewModel?> GetMemberHealthRecordDetailsAsync(int memberid, CancellationToken cancellationToken)
    {
        var record =await _healthRecordRepository.FirstOrDefaultAsync(hr => hr.MemberId == memberid, cancellationToken: cancellationToken);
        // map 
        if (record == null) 
            return null;
        return new HealthRecordViewModel
        {
            Weight = record.weight,
            Height = record.height,
            BloodType = record.bloodType,
            Note = record.Note
        };
        
    }

    public async Task<MemberEditViewModel?> GetMemberDetailsForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null)
            return null;
        return new MemberEditViewModel
        {
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            BuildingNumber = member.Address.BuildingNumber,
            Street = member.Address.Street,
            City = member.Address.City
        };
    }

    public async Task<bool> UpdateMemberAsync(int id, MemberEditViewModel memberViewModel, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null) 
            return false;

        var emailExists = await _memberRepository.AnyAsync(m => m.Email == memberViewModel.Email && m.Id != id, cancellationToken);
        var phoneExists = await _memberRepository.AnyAsync(m => m.Phone == memberViewModel.Phone && m.Id != id, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        // Update the member entity with the new values from the view model
        member.Email = memberViewModel.Email;
        member.Phone = memberViewModel.Phone;


        member.Address.BuildingNumber = memberViewModel.BuildingNumber;
        member.Address.Street = memberViewModel.Street;
        member.Address.City = memberViewModel.City;
        // Save the changes to the database
        return ( await _memberRepository.UpdateAsync(member, cancellationToken: cancellationToken)) > 0;
    }

    public async Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null)
            return false;

        var hasActiveSession = await _bookingRepository.AnyAsync(b => b.MemberId == id && b.Session.StartDate >= DateTime.Now,
            cancellationToken: cancellationToken);

        if (hasActiveSession) return false;

        var result = (await _memberRepository.DeleteAsync(member, cancellationToken)) > 0;

        if (result)
        {

            return true;
        }
        return false;
      
    }
}
