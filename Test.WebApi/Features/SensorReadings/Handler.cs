using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.WebApi.Features.SensorReadings
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
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }

            var fromDateQuery = !request.FromDate.HasValue
                ? string.Empty
                : "[TimestampUtc] >= @fromDate";

            var toDateQuery = !request.ToDate.HasValue
                ? string.Empty
                : "[TimestampUtc] <= @toDate";

            var whereClause = (request.FromDate.HasValue && request.ToDate.HasValue)
                ? $"{fromDateQuery} AND {toDateQuery} AND "
                : request.FromDate.HasValue 
                    ? fromDateQuery + " AND "  
                    : request.ToDate.HasValue 
                        ? toDateQuery + " AND "
                        : string.Empty;

            var query = $"select [DeviceRegistrationId] as [DeviceId], [SensorId] as [Type], [TimestampUtc], [Value] [RawValue] from [sensors].[SensorReadings] where {whereClause} [DeviceRegistrationId] = @deviceId "; 
            //Console.WriteLine("Query: {0}\r\nFrom Date: {1}\r\nTo Date: {2}\r\n", query, request.FromDate, request.ToDate);
            var sensorReadings = await dbConnection.QueryAsync<Models.SensorReading>(query,
                new { deviceId = request.SensorId, fromDate = request.FromDate?.UtcDateTime, toDate = request.ToDate?.UtcDateTime });

            return new Response(sensorReadings);
        }
    }
}
