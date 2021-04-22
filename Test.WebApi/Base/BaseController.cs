using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi.Base
{
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator mediator;

        public BaseController(IMediator mediator = default)
        {
            this.mediator = mediator;
        }

        protected IMediator Mediator => mediator ??= GetService<IMediator>();

        private IMediator GetService<TService>()
        {
            return HttpContext.RequestServices
                .GetRequiredService<IMediator>();
        }
    }
}
