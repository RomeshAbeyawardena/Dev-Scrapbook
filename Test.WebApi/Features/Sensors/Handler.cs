using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models = Test.WebApi.Models;
namespace Test.WebApi.Features.Sensors
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IDbConnection dbConnection;

        public Handler(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            if(dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }

            var optionalWhereQuery = string.IsNullOrWhiteSpace(request.Tag) 
                ? string.Empty 
                : "[Tags] = @tags AND";

            var query = $"select [Id], [DisplayName], [SerialNumber], [Tags] from [sensors].[DeviceRegistrations] where {optionalWhereQuery} [IsEnabled] = 1";
            //Console.WriteLine(query);
            var sensors = await dbConnection.QueryAsync<Models.Sensor>(query,
                new { tags = request.Tag });

            return new Response(sensors);
        }
    }
}
