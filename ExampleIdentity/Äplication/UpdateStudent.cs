﻿using ExampleIdentity.Core.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleIdentity.Äplication
{
    public class UpdateStudent
    {
        public class ExecuteUpdateStudent : IRequest
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Subjects { get; set; }
            public int Age { get; set; }
            public string Phone { get; set; }
            public string Marks { get; set; }
        }

        public class ValidationUpdateStudent : AbstractValidator<ExecuteUpdateStudent>
        {
            public ValidationUpdateStudent()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
                RuleFor(x => x.Firstname).NotEmpty().NotNull();
                RuleFor(x => x.Lastname).NotEmpty().NotNull();
                RuleFor(x => x.Subjects).NotEmpty().NotNull();
                RuleFor(x => x.Age).NotEmpty().NotNull();
                RuleFor(x => x.Phone).NotEmpty().NotNull();
                RuleFor(x => x.Marks).NotEmpty().NotNull();
            }
        }

        public class HandlerUpdate : IRequestHandler<ExecuteUpdateStudent>
        {
            private readonly ExampleEntityContext context;

            public HandlerUpdate(ExampleEntityContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(ExecuteUpdateStudent request, CancellationToken cancellationToken)
            {

                var validationUpdate = await context.Student.Where(x => x.IdStudent == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                if (validationUpdate == null)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });

                }

                validationUpdate.Firstname = request.Firstname;
                validationUpdate.Lastname = request.Lastname;
                validationUpdate.Marks = request.Marks;
                validationUpdate.Age = request.Age;
                validationUpdate.Phone = request.Phone;
                validationUpdate.Subjects = request.Subjects;
                context.Entry(validationUpdate).State = EntityState.Modified;
                var response = await context.SaveChangesAsync(cancellationToken);
                if (response > 0)
                {
                    return Unit.Value;
                }

                throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not update" });
            }
        }
    }
}
