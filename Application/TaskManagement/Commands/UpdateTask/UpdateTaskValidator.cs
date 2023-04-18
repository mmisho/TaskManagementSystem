
using FluentValidation;

namespace Application.TaskManagement.Commands.UpdateTask
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTask>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
        }
    }
}
