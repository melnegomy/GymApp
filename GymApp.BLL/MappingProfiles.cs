using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.BLL;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Member, MemberViewModel>();
        CreateMap<MemberCreateViewModel, Member>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                Street = src.Street,
                City = src.City
            }));        
        CreateMap <HealthRecordViewModel, HealthRecord>();
        CreateMap <Member, MemberDetailsViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}, {src.Address.City}"))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));
    }
}
