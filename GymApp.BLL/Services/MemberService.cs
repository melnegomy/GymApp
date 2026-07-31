using GymApp.DAL.Contracts;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.VisualBasic;

namespace GymApp.BLL.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken : cancellationToken);

        var result = members.Select(m => new MemberViewModel
        {
            Id = m.Id,
            Name = m.Name,
            Phone= m.Phone, 
            Gender = m.Gender.ToString(),
            Photo= m.photo,
            Email = m.Email
        }).ToList();


        return result;
    }

    public async Task<bool> CreateMemberAsync(CreateMemberViewModel createMemberViewModel, CancellationToken cancellationToken = default)
    {
        //var members = await _memberRepository.GetAllAsync(cancellationToken: cancellationToken);
        //var emailExists = members.Any(m=>m.Email == createMemberViewModel.Email);
        //var phoneExists = members.Any(m=>m.Phone == createMemberViewModel.Phone);
        //if (emailExists || phonesExists)
        //    return false;

        var emailExists = await _memberRepository.EmailExistsAsync(createMemberViewModel.Email, cancellationToken);
        var phoneExists = await _memberRepository.PhoneExistsAsync(createMemberViewModel.Phone, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        var member = new Member
        {
            Name = createMemberViewModel.Name,
            Email = createMemberViewModel.Email,
            Phone = createMemberViewModel.Phone,
            DateOfBirth = createMemberViewModel.DateOfBirth,
            Gender = createMemberViewModel.Gender,
            photo="",
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

        
        return ( await _memberRepository.AddAsync(member, cancellationToken: cancellationToken)) > 0;
    }

}
