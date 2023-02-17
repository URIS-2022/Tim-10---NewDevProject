using Logger.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Logger.Controllers
{
    [ApiController]
    [Route("api/logger")]
    public class LoggerController : ControllerBase
    {
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerController() { }

        [HttpPost]
        public void PostLogger([FromBody] LogModel model)
        {
            if(model.Information == "Info")
            {
                logger.Info("Method name: " + model.Method + ", Service name: " + model.ServiceName + ", message" + model.Error);
            } else if (model.Information == "Warn")
            {
                logger.Warn("Method name: " + model.Method + ", Service name: " + model.ServiceName + ", message" + model.Error);
            }
            else
            {
                logger.Error("Method name: " + model.Method + ", Service name: " + model.ServiceName + ", message" + model.Error);
            }
        }
    }
}
