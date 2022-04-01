using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExampleIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyControllerBase : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
