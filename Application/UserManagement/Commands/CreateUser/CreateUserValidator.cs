using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            this.RuleFor(x => x.FirstName).NotEmpty().NotNull();
            this.RuleFor(x => x.LastName).NotEmpty().NotNull();
            this.RuleFor(x => x.Email).EmailAddress();
            this.RuleFor(x => x.IdNumber).NotEmpty().NotNull();
            this.RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
