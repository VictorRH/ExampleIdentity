using ExampleIdentity.Äplication;
using ExampleIdentity.Core.Dto;
using ExampleIdentity.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleIdentity.Controllers
{
    public class ExampleIdentityController : MyControllerBase
    {
       
        [HttpGet("{id}")]
        public async Task<ActionResult<StudenModelDto>> GetStudentId(int id)
        {
            return await Mediator.Send(new StudentId.ExecuteStudentId { Id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteStudent(int id)
        {
            return await Mediator.Send(new DeleteStudentId.ExecuteDeleteStudent { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddStudent(NewStudent.ExecuteNewStudent data)
        {
            return await Mediator.Send(data);
        }
        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateStudent(UpdateStudent.ExecuteUpdateStudent data)
        {
            return await Mediator.Send(data);
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<List<StudenModelDto>>> PaginationStudents(PaginationStudent.ExecutePagination data)
        {
            return await Mediator.Send(data);
        }



    }
}
