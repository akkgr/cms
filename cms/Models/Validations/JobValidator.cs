using FluentValidation;

namespace cms.Models.Validations
{
    public class JobValidator: AbstractValidator<Job>
    {
        public JobValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
            RuleFor(t => t.PersonId).NotEmpty().WithMessage("Δεν είναι συμπληρωμένος ο πελάτης");
            RuleFor(t => t.Description).NotEmpty().WithMessage("Δεν είναι συμπληρωμένη η εργασία");
            RuleFor(t => t.Implemented).NotNull().WithMessage("Δεν είναι συμπληρωμένη η ημερομηνία");
            RuleFor(t => t.Amount).GreaterThanOrEqualTo<Job,decimal>(0).WithMessage("Δεν είναι συμπληρωμένη το ποσό");
        }
    }
}
