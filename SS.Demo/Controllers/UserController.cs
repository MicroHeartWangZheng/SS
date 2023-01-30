using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using SS.Demo.Entity;

namespace SS.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;

        private readonly ISqlSugarClient modelClient;

        private readonly ISqlSugarClient basicClient;

        private readonly EventIdProvider eventIdProvider;

        public UserController(ILogger<UserController> logger, ISqlSugarClient sqlSugarClients, EventIdProvider eventIdProvider)
        {
            this.logger = logger;
            //this.modelClient = sqlSugarClients.AsTenant().GetConnection("Model");
            this.basicClient = sqlSugarClients.AsTenant().GetConnection("Basic");
            this.eventIdProvider = eventIdProvider;
        }

        [HttpGet]
        public async Task GetUserListAsync()
        {
            logger.LogInformation($"请求接口时的 EventId:{eventIdProvider.EventId}");
            await basicClient.Queryable<IkWordEntity>().ToListAsync();
            //await modelClient.Queryable<UserEntity>().ToListAsync();
        }

    }
}