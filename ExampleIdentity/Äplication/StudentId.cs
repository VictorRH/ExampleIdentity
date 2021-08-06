using ExampleIdentity.Core.Dto;
using ExampleIdentity.Core.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleIdentity.Äplication
{
    public class StudentId
    {
        public class ExecuteStudentId : IRequest<StudenModelDto>
        {
            public int Id { get; set; }
        }

        public class ValidationStudentId : AbstractValidator<ExecuteStudentId>
        {
            public ValidationStudentId()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<ExecuteStudentId, StudenModelDto>
        {
            private readonly ExampleEntityContext context;

            public Handler(ExampleEntityContext context)
            {
                this.context = context;
            }

            public async Task<StudenModelDto> Handle(ExecuteStudentId request, CancellationToken cancellationToken)
            {
                var result = await context.Student.Where(x => x.IdStudent == request.Id).FirstOrDefaultAsync(cancellationToken);

                if (result == null)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });

                }
                return new StudenModelDto
                {
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    Age = result.Age,
                    Datecreated = result.Datecreated,
                    Marks = result.Marks,
                    Phone = result.Phone,
                    Subjects = result.Subjects
                };


            }
        }
    }
}
