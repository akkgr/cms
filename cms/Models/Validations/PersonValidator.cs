using FluentValidation;

namespace cms.Models.Validations
{
    public class PersonValidator: AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
            RuleFor(t => t.Lastname).NotEmpty().WithMessage("Δεν είναι συμπληρωμένο το επώνυμο");
            RuleFor(t => t.Lastname).Length(1,100).WithMessage("Tο επώνυμο χωράει μέχρι 100 χαρακτήρες.");
            RuleFor(t => t.Firstname).NotEmpty().WithMessage("Δεν είναι συμπληρωμένο το όνομα");
            RuleFor(t => t.Firstname).Length(1,100).WithMessage("Tο όνομα χωράει μέχρι 100 χαρακτήρες.");
        }
    }
}
