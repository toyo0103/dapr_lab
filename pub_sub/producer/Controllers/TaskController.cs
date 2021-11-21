using System;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using producer.Controllers.Parameter;

namespace producer.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<TaskController> _logger;

        public TaskController(
            DaprClient daprClient,
            ILogger<TaskController> logger)
        {
            this._daprClient = daprClient;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<Guid> CreateAsync(CreateTaskParameter para)
        {
            var task = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Data = para.Data,
                JobName = para.JobName
            };
            _logger.LogInformation(para.Data);
            try
            {
                await _daprClient.PublishEventAsync<TaskDTO>("pubsub-rq", "newOrder", task);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex,ex.Message);
            }
            return task.Id;
        }
    }
}
