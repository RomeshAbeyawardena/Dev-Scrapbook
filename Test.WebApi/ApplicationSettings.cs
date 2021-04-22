using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WebApi
{
    public class ApplicationSettings
    {
        public ApplicationSettings(IConfiguration configuration)
        {
            DefaultConnectionString = configuration.GetConnectionString("default");
        }

        public string DefaultConnectionString { get; set; }
    }
}
