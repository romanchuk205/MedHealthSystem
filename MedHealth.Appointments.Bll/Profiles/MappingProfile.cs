using AutoMapper;
using MedHealth.Appointments.Bll.Models;
using MedHealth.Appointments.Domain;

namespace MedHealth.Appointments.Bll.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Перетворення з Бази в DTO
        CreateMap<Patient, PatientDto>();

        // Перетворення з DTO створення в Сутність бази
        CreateMap<CreatePatientDto, Patient>();
    }
}