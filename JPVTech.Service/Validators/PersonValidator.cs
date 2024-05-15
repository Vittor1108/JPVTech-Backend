using FluentValidation;
using JPVTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Service.Validators
{
    public class PersonValidator : AbstractValidator<PersonEntity>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .NotNull()
                                .WithMessage("Informe o nome. Tente novamente.")
                                .MaximumLength(180)
                                .WithMessage("Quantidade de caracteres máximo para o nome de uma pessoa é de 180. Tente novametne.")
                                .WithErrorCode("422");

            RuleFor(x => x.CPF).NotEmpty()
                .NotNull()
                .WithMessage("Informar o CPF. Tente novamente.")
                .MaximumLength(11)
                .WithMessage("CPF inválido. Tente novamente.")
                .WithErrorCode("422");

            RuleFor(x => x.IncomeValue)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a renda mensal e tente novamente!")
                .WithErrorCode("422");

            RuleFor(x => x.DateBirth)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a data de nascimento e tente novamente.")
                .WithErrorCode("422");
        }
    }
}
