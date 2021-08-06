using ExampleIdentity.Core.Persistence;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleIdentity.Äplication
{
    public class DeleteStudentId
    {
        public class ExecuteDeleteStudent : IRequest
        {
            public int Id { get; set; }
        }

        public class ValidationStudentId : AbstractValidator<ExecuteDeleteStudent>
        {
            public ValidationStudentId()
            {
                RuleFor(x => x.Id).NotEmpty().NotNull();
            }
        }

        public class HandlerDelete : IRequestHandler<ExecuteDeleteStudent>
        {
            private readonly ExampleEntityContext context;

            public HandlerDelete(ExampleEntityContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(ExecuteDeleteStudent request, CancellationToken cancellationToken)
            {
                var result = context.Student.Where(x => x.IdStudent == request.Id).FirstOrDefault();
                if (result == null)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });

                }

                context.Student.Remove(result);
                var resultDelete = await context.SaveChangesAsync(cancellationToken);
                if (resultDelete > 0)
                {
                    return Unit.Value;
                }

                throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not delete" });

            }
        }
    }
}
