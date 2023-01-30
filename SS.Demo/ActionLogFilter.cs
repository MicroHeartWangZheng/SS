using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace SS.Demo
{
    public class ActionLogFilter : IActionFilter
    {
        private readonly ILogger<ActionLogFilter> logger;

        private readonly EventIdProvider eventIdProvider;



        public ActionLogFilter(ILogger<ActionLogFilter> logger, EventIdProvider eventIdProvider)
        {
            this.logger = logger;
            this.eventIdProvider = eventIdProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null || context.Result is Microsoft.AspNetCore.Mvc.FileStreamResult)
                return;
            logger.LogInformation($"返回： EventId:{eventIdProvider.EventId}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"请求：EventId:{eventIdProvider.EventId}");
        }
    }
}
