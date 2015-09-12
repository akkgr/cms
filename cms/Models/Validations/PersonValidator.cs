using FluentValidation;

namespace cms.Models.Validations
{
    public class PersonValidator: AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(t => t.Id).NotNull();
            RuleFor(t => t.Lastname).NotNull();
            RuleFor(t => t.Firstname).NotNull();
        }
    }
}
