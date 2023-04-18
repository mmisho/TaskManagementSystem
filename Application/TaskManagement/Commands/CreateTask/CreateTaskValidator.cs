using FluentValidation;

namespace Application.TaskManagement.Commands.CreateTask
{
    public class CreateTaskValidator : AbstractValidator<CreateTask>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
        }
    }
}
