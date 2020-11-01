using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Assignment1_MuhammadHafiz.Validator
{
    public class StationValidator : AbstractValidator<Assignment1_MuhammadHafiz.Models.Ticket>
    {
        public StationValidator()
        { 
             RuleFor(x => x.EndPoint).NotEqual(x => x.StartPoint).WithMessage("Same origin and destination");
        }
    }
}