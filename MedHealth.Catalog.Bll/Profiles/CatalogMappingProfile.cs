using AutoMapper;
using MedHealth.Catalog.Bll.Models;
using MedHealth.Catalog.Domain;

namespace MedHealth.Catalog.Bll.Profiles;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        // 1. Маппінг для читання (Entity -> DTO)
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.SpecializationTitle, 
                opt => opt.MapFrom(src => src.Specialization != null ? src.Specialization.Title : "Не вказано"))
            .ForMember(dest => dest.OfficeRoomNumber, 
                opt => opt.MapFrom(src => src.Office != null ? src.Office.RoomNumber : "Не вказано"));

        // 2. Маппінг для створення (CreateDTO -> Entity)
        CreateMap<CreateDoctorDto, Doctor>();
    }
}