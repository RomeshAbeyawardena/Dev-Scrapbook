using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi.Features.Sensors
{
    public class Query : IRequest<Response>
    {
        public string Tag { get; set; }
    }
}
