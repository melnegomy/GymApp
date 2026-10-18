using GymApp.BLL.ViewModels.Session;
using GymApp.DAL.Models.Enums;
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
        CreateMembersMap();
        CreateMap<Session, SessionViewModel>()
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
        CreateMap<CreateSessionViewModel, Session>(); 
        CreateMap<Category, CategorySelectItemViewModel>();
    }

    private void CreateMembersMap()
    {
        CreateMap<Member, MemberViewModel>();
        CreateMap<MemberCreateViewModel, Member>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                Street = src.Street,
                City = src.City
            }));
        CreateMap<HealthRecordViewModel, HealthRecord>();
        CreateMap<Member, MemberDetailsViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}, {src.Address.City}"))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));

        CreateMap<Trainer, TrainerViewModel>();
        CreateMap<TrainerCreateViewModel, Trainer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                Street = src.Street,
                City = src.City
            }))
            .ForMember(
        dest => dest.Specialty,
           opt => opt.MapFrom(src => Enum.Parse<Specialties>(src.Specialties)));
        CreateMap<Trainer, TrainerDetailsViewModel>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}, {src.Address.City}"));

        CreateMap<Trainer, TrainerEditViewModel>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialty))
            .ReverseMap();
    }
}
