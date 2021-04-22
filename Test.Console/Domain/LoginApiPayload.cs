using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Domain
{
    public class LoginApiPayload
    {
        public string Assertion { get; set; }
        public string Grant_Type { get; set; }
    }
}
