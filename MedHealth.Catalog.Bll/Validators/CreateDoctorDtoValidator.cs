using FluentValidation;
using MedHealth.Catalog.Bll.Models;

namespace MedHealth.Catalog.Bll.Validators;

public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
{
    public CreateDoctorDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ім'я лікаря обов'язкове")
            .MaximumLength(100).WithMessage("Ім'я не може бути довшим за 100 символів");

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Досвід не може бути від'ємним");

        RuleFor(x => x.SpecializationId)
            .GreaterThan(0).WithMessage("Потрібно вибрати спеціалізацію (ID > 0)");
            
        RuleFor(x => x.OfficeId)
            .GreaterThan(0).WithMessage("Потрібно вибрати кабінет (ID > 0)");
    }
}