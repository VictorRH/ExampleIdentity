using ExampleIdentity.Core.Entities;
using ExampleIdentity.Core.Persistence;
using FluentValidation;
using MediatR;
using System.Net;

namespace ExampleIdentity.Aplication
{
    public class NewStudent
    {
        public class ExecuteNewStudent : IRequest
        {
            public string Firstname { get; set; } = string.Empty;
            public string Lastname { get; set; } = string.Empty;
            public string Subjects { get; set; } = string.Empty;
            public int Age { get; set; }
            public string Phone { get; set; } = string.Empty;
            public string Marks { get; set; } = string.Empty;
        }

        public class ValidationAddStudent : AbstractValidator<ExecuteNewStudent>
        {
            public ValidationAddStudent()
            {
                RuleFor(x => x.Firstname).NotEmpty().NotNull();
                RuleFor(x => x.Lastname).NotEmpty().NotNull();
                RuleFor(x => x.Subjects).NotEmpty().NotNull();
                RuleFor(x => x.Age).NotEmpty().NotNull();
                RuleFor(x => x.Phone).NotEmpty().NotNull();
                RuleFor(x => x.Marks).NotEmpty().NotNull();
            }
        }
        public class HandlerAdd : IRequestHandler<ExecuteNewStudent>
        {
            private readonly ExampleEntityContext context;
            public HandlerAdd(ExampleEntityContext context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(ExecuteNewStudent request, CancellationToken cancellationToken)
            {
                var addStudent = new StudentModel
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Age = request.Age,
                    Datecreated = DateTime.UtcNow,
                    Marks = request.Marks,
                    Phone = request.Phone,
                    Subjects = request.Subjects
                };
                await context.Student.AddAsync(addStudent, cancellationToken);
                var resultAdd = await context.SaveChangesAsync(cancellationToken);
                if (resultAdd > 0)
                {
                    return Unit.Value;
                }
                throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: Failed insert new student" });
            }
        }
    }
}
