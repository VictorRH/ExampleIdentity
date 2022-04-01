using ExampleIdentity.Core.Dto;
using ExampleIdentity.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExampleIdentity.Aplication
{
    public class GetAllStudents 
    {
        public record ExecuteAllStudent():IRequest<List<StudenModelDto>> ;
        public class HandlerAll : IRequestHandler<ExecuteAllStudent, List<StudenModelDto>>
        {
            private readonly ExampleEntityContext context;
            public HandlerAll(ExampleEntityContext context)
            {
                this.context = context;
            }
            public async Task<List<StudenModelDto>> Handle(ExecuteAllStudent request, CancellationToken cancellationToken)
            {
                return await context.Student.Select(x => new StudenModelDto()
                {
                    Id = x.IdStudent,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Phone = x.Phone,
                    Subjects = x.Subjects,
                    Marks = x.Marks,
                    Age = x.Age,

                }).ToListAsync(cancellationToken);
            }
        }
    }
}
