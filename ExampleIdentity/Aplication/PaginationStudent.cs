using ExampleIdentity.Core.Dto;
using ExampleIdentity.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExampleIdentity.Aplication
{
    public class PaginationStudent
    {
        public class ExecutePagination : IRequest<List<StudenModelDto>>
        {
            public string StudentStorage { get; set; } = string.Empty;
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
        }

        public class HandlerPagination : IRequestHandler<ExecutePagination, List<StudenModelDto>>
        {
            private readonly ExampleEntityContext context;
            public HandlerPagination(ExampleEntityContext context)
            {
                this.context = context;
            }
            public async Task<List<StudenModelDto>> Handle(ExecutePagination request, CancellationToken cancellationToken)
            {
                request.PageNumber = request.PageNumber == 0 ? 1 : request.PageNumber;
                request.PageSize = request.PageSize == 0 ? 10 : request.PageSize;
                var student = string.IsNullOrEmpty(request.StudentStorage);

                var totalStudents = await context.Student.ToListAsync(cancellationToken: cancellationToken);
                if (totalStudents.Count <= 0)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student database is empty" });
                }
                var result = (student) ? await context.Student.Select(x => new StudenModelDto
                {
                    Id = x.IdStudent,
                    Age = x.Age,
                    Datecreated = x.Datecreated,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Marks = x.Marks,
                    Phone = x.Phone,
                    Subjects = x.Subjects
                }).OrderByDescending(x => x.Datecreated).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken) :

                await context.Student.Where(x => x.Firstname!.Contains(request.StudentStorage)).Select(x => new StudenModelDto
                {
                    Id = x.IdStudent,
                    Age = x.Age,
                    Datecreated = x.Datecreated,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Marks = x.Marks,
                    Phone = x.Phone,
                    Subjects = x.Subjects

                }).OrderByDescending(x => x.Datecreated).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);

                if (result.Count <= 0)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new { message = "Error: student not found" });
                }
                var studentPagination = new List<StudenModelDto> {
                    new StudenModelDto
                    {
                        StudentList = result,
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize,
                        TotalRecords = totalStudents.Count
                    }
               };
                return studentPagination;
            }
        }
    }
}
