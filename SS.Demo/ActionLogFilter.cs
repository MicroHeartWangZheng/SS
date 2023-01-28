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
            logger.LogInformation($"请求返回： EventId:{eventIdProvider.EventId} \r\n {JsonConvert.SerializeObject(context.Result)}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"请求地址：EventId:{eventIdProvider.EventId} \r\n {context.HttpContext.Request.Path} \r\n" +
                                                     $"请求方式：{context.HttpContext.Request.Method} \r\n" +
                                                     $"请求参数：{JsonConvert.SerializeObject(context.ActionArguments.Values)}");
        }
    }
}
