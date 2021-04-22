using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ConsoleApp.Domain;
using Test.ConsoleApp.Extensions;

namespace Test.ConsoleApp
{
    public class DisruptiveTechAPIClient
    {
        private const string LoginUrl = "https://identity.disruptive-technologies.com/oauth2/token";
        private const string LoginGrantJwtBearerType = "urn:ietf:params:oauth:grant-type:jwt-bearer";
        private const string GetProjectDevicesUrl = "projects/{0}/devices";
        private const string GetProjectDeviceEventsUrl = "projects/{0}/devices/{1}/events";
        private const string ExpectedApiDateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
        private const string StartTimeParameter = "start_time";
        private const string EndTimeParameter = "end_time";
        private const string PageSizeParameter = "page_size";
        private const string ContentTypeUrlEncodedForm = "application/x-www-form-urlencoded";

        private readonly RestClient client;
        private readonly string issuer;
        private readonly string apiKey;
        private readonly string apiKeySecret;
        private readonly string projectId;
        private readonly IJwtEncoder jwtEncoder;

        public DisruptiveTechAPIClient(string baseUrl, string issuer,
            string apiKey, string apiKeySecret, string projectId,
            IJwtEncoder jwtEncoder)
        {
            client = new RestClient(baseUrl);
            client.UseSerializer<JsonNetSerializer>();
            this.issuer = issuer;
            this.apiKey = apiKey;
            this.apiKeySecret = apiKeySecret;
            this.projectId = projectId;
            this.jwtEncoder = jwtEncoder;
        }

        public void UseLoginApiAcccessToken(LoginApiAccessToken token)
        {
            client.Authenticator = new JwtAuthenticator(token.AccessToken);
        }

        public Task<GetProjectDevicesResponse> GetProjectDevices()
        {
            var restSharpRequest = new RestRequest(
                string.Format(GetProjectDevicesUrl, projectId),
                Method.GET,
                DataFormat.Json);

            return client.GetAsync<GetProjectDevicesResponse>(restSharpRequest);
        }
        //async task
        public Task<GetProjectDeviceEventsResponse> GetProjectDeviceEvents(string deviceId,
            DateTime startDate, DateTime endDate, int pageSize = 1000)
        {
            var restSharpRequest = new RestRequest(
                string.Format(GetProjectDeviceEventsUrl, projectId, deviceId),
                Method.GET,
                DataFormat.Json);

            restSharpRequest
                .AddParameter(StartTimeParameter,
                    startDate.ToString(ExpectedApiDateTimeFormat),
                    ParameterType.QueryString);

            restSharpRequest
                .AddParameter(EndTimeParameter,
                    endDate.ToString(ExpectedApiDateTimeFormat),
                    ParameterType.QueryString);

            restSharpRequest
                .AddParameter(PageSizeParameter,
                    pageSize, ParameterType.QueryString);

            return client
                .GetAsync<GetProjectDeviceEventsResponse>(restSharpRequest);
        }

        public Task<LoginApiAccessToken> Login()
        {
            var loginJwtToken = GenerateJwtToken(issuer, LoginUrl);

            var loginClient = new RestClient();

            var loginRequest = new RestRequest(LoginUrl, Method.POST);
            loginRequest
                .AddParameter("content-type", ContentTypeUrlEncodedForm, ParameterType.HttpHeader);

            loginRequest
                .AddParameter("assertion", loginJwtToken, ParameterType.GetOrPost);

            loginRequest
                .AddParameter("grant_type", LoginGrantJwtBearerType, ParameterType.GetOrPost);

            return loginClient.PostAsync<LoginApiAccessToken>(loginRequest);
        }

        private string GenerateJwtToken(string issuer, string audience)
        {
            var dateNow = DateTime.Now;

            var headers = new Dictionary<string, object>()
            {
                { "kid", apiKey }
            };

            var payload = new Dictionary<string, object>()
            {
                { "iat", Math.Floor((decimal)dateNow.ToUnixTime()) },
                { "exp", Math.Floor((decimal)dateNow.ToUnixTime() + 3600)  },
                { "aud", audience },
                { "iss", issuer }
            };

            return jwtEncoder
                .Encode(headers, payload, apiKeySecret);
        }
    }
}
