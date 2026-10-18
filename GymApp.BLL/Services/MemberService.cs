using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.VisualBasic;

namespace GymApp.BLL.Services;

public class MemberService : IMemberService
{
    //private readonly IGenericRepository<Member> _memberRepository;
    //private readonly IGenericRepository<MemberShip> _memberShipRepository;
    //private readonly IGenericRepository<Plan> _planRepository;
    //private readonly IGenericRepository<HealthRecord> _healthRecordRepository;
    //private readonly IGenericRepository<Booking> _bookingRepository;

    //public MemberService(IGenericRepository<Member> memberRepository, IGenericRepository<MemberShip> memberShipRepository, IGenericRepository<Plan> planRepository, IGenericRepository<HealthRecord> healthRecordRepository, IGenericRepository<Booking> bookingRepository)
    //{
    //    _memberRepository = memberRepository;
    //    _memberShipRepository = memberShipRepository;
    //    _planRepository = planRepository;
    //    _healthRecordRepository = healthRecordRepository;
    //    _bookingRepository = bookingRepository;
    //}

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default)
    {
        var members = await _unitOfWork.GetRepository<Member>()
            .GetAllAsync(cancellationToken: cancellationToken);

       return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(members);
    }

    public async Task<bool> CreateMemberAsync(MemberCreateViewModel createMemberViewModel, CancellationToken cancellationToken = default)
    {
        var _memberrepository = _unitOfWork.GetRepository<Member>();
        var emailExists = await _memberrepository
            .AnyAsync(m => m.Email == createMemberViewModel.Email, cancellationToken);
        var phoneExists = await _memberrepository
            .AnyAsync(m => m.Phone == createMemberViewModel.Phone, cancellationToken);
        if (emailExists || phoneExists)
            return false;

      /*  //var member = new Member
        //{
        //    Name = createMemberViewModel.Name,
        //    Email = createMemberViewModel.Email,
        //    Phone = createMemberViewModel.Phone,
        //    DateOfBirth = createMemberViewModel.DateOfBirth,
        //    Gender = createMemberViewModel.Gender,
        //    photo = "",
        //    HealthRecord = new HealthRecord
        //    {
        //        height = createMemberViewModel.HealthRecord.Height,
        //        weight = createMemberViewModel.HealthRecord.Weight,
        //        bloodType = createMemberViewModel.HealthRecord.BloodType,
        //        Note = createMemberViewModel.HealthRecord.Note,
        //    },
        //    Address = new Address
        //    {
        //        BuildingNumber = createMemberViewModel.BuildingNumber,
        //        Street = createMemberViewModel.Street,
        //        City = createMemberViewModel.City
        //    }
        //};*/


        var member = _mapper.Map<Member>(createMemberViewModel);

        _memberrepository.Add(member, cancellationToken: cancellationToken);
        return (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;
    }

    public async Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        var _memberrepository = _unitOfWork.GetRepository<Member>();

        var member = await _memberrepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null) return null;
        
        //var memberDetailsViewModel = _mapper.Map<MemberDetailsViewModel>(member);
        var memberDetailsViewModel = _mapper.ProjectTo<MemberDetailsViewModel>(
            _unitOfWork.GetRepository<Member>().GetQueryable()).FirstOrDefault(member => member.Id == id);

        var memberShip = await _unitOfWork.GetRepository<MemberShip>().FirstOrDefaultAsync(m => m.MemberId == memberDetailsViewModel.Id && m.EndDate > DateOnly.FromDateTime(DateTime.Now), cancellationToken: cancellationToken);
        if (memberShip is not null) 
        {
        var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(memberShip.PlanId, cancellationToken: cancellationToken);

            memberDetailsViewModel.MemberShipEndDate = memberShip.EndDate.ToShortDateString();
            memberDetailsViewModel.MemberShipStartDate = memberShip.CreatedAt?.ToShortDateString();
            memberDetailsViewModel.PlanName = plan?.Name ?? "No Plan";


        }

        return memberDetailsViewModel;
    }

    public async Task<HealthRecordViewModel?> GetMemberHealthRecordDetailsAsync(int memberid, CancellationToken cancellationToken)
    {

        var record =await _unitOfWork.GetRepository<HealthRecord>().FirstOrDefaultAsync(hr => hr.MemberId == memberid, cancellationToken: cancellationToken);
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

        var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id, cancellationToken: cancellationToken);
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
        var _memberrepository = _unitOfWork.GetRepository<Member>();
        var member = await _memberrepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null) 
            return false;

        var emailExists = await _memberrepository.AnyAsync(m => m.Email == memberViewModel.Email && m.Id != id, cancellationToken);
        var phoneExists = await _memberrepository.AnyAsync(m => m.Phone == memberViewModel.Phone && m.Id != id, cancellationToken);
        if (emailExists || phoneExists)
            return false;

        // Update the member entity with the new values from the view model
        member.Email = memberViewModel.Email;
        member.Phone = memberViewModel.Phone;


        member.Address.BuildingNumber = memberViewModel.BuildingNumber;
        member.Address.Street = memberViewModel.Street;
        member.Address.City = memberViewModel.City;
        _memberrepository.Update(member, cancellationToken: cancellationToken);
        return (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;
    }

    public async Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken)
    {
        var _memberrepository = _unitOfWork.GetRepository<Member>();
        var member = await _memberrepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (member == null)
            return false;

        var hasActiveSession = await _unitOfWork.GetRepository<Booking>().AnyAsync(b => b.MemberId == id && b.Session.StartDate >= DateTime.Now,
            cancellationToken: cancellationToken);

        if (hasActiveSession) return false;
        _memberrepository.Delete(member, cancellationToken);
        var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;

        if (result)
        {

            return true;
        }
        return false;
      
    }
}
