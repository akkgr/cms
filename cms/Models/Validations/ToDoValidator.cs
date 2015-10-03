using FluentValidation;

namespace cms.Models.Validations
{
    public class ToDoValidator: AbstractValidator<ToDo>
    {
        public ToDoValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
            RuleFor(t => t.Description).NotEmpty().WithMessage("Δεν είναι συμπληρωμένη η Περιγραφή");
            RuleFor(t => t.ToDoDate).NotNull().WithMessage("Δεν είναι συμπληρωμένη η Ημερομηνία");
            RuleFor(t => t.PersonId).NotEmpty().WithMessage("Δεν είναι συμπληρωμένος ο Πελάτης");
            RuleFor(t => t.Done).NotNull();
        }
    }
}
