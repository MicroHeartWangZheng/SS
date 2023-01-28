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

        public UserController(ILogger<UserController> logger, IEnumerable<ISqlSugarClient> sqlSugarClients, EventIdProvider eventIdProvider)
        {
            this.logger = logger;
            this.modelClient = sqlSugarClients.FirstOrDefault(x => x.CurrentConnectionConfig.ConfigId == "Model");
            this.basicClient = sqlSugarClients.FirstOrDefault(x => x.CurrentConnectionConfig.ConfigId == "Basic");
            this.eventIdProvider = eventIdProvider;
        }

        [HttpGet]
        public async Task<List<UserEntity>> GetUserListAsync()
        {
            logger.LogInformation($"请求了接口GetUserListAsync EventId:{eventIdProvider.EventId}");
            var a = await basicClient.Queryable<IkWordEntity>().ToListAsync();
            return await modelClient.Queryable<UserEntity>().ToListAsync();
        }


        [HttpGet("Basic")]
        public async Task<List<IkWordEntity>> GetBasicListAsync()
        {
            logger.LogInformation($"请求了接口GetBasicListAsync EventId:{eventIdProvider.EventId}");
            return await basicClient.Queryable<IkWordEntity>().ToListAsync();
        }
    }
}