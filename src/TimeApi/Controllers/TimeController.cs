using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TimeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;
        private readonly string _applicationName;
        private readonly string _requestUrl;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;

            _applicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME") ?? "Dev";
            _requestUrl = Environment.GetEnvironmentVariable("REQUEST_URL") ?? "http://time.jsontest.com";
        }

        [HttpGet]
        public async Task<TimeResult> Get()
        {
            _logger.LogInformation($"Application name: {_applicationName}.");
            _logger.LogInformation($"Requesting URL {_requestUrl}");

            var time = await GetTime();

            if (string.IsNullOrEmpty(time.ApplicationName))
            {
                time.ApplicationName = _applicationName;
                return time;
            }

            return new TimeResult
            {
                Response = time,
                ApplicationName = _applicationName
            };
        }

        private async Task<TimeResult> GetTime()
        {
            var sw = new Stopwatch();
            sw.Start();
            var client = new HttpClient();
            var response = await client.GetAsync(_requestUrl);
            var result = await response.Content.ReadAsAsync<TimeResult>();
            result.ElapsedMs = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }
    }
}
