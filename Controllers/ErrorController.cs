using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            var statuscodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested couldn't be found";
                    //ViewBag.path = statuscodeResult.OriginalPath;
                    //ViewBag.QS = statuscodeResult.OriginalQueryString;
                    logger.LogWarning($"404 error occured at Path={statuscodeResult.OriginalPath}"
                        + $"and Querystring={statuscodeResult.OriginalQueryString}");
                    break;
            }

            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.ExceptionStackTrace = exceptionDetails.Error.StackTrace;
            logger.LogError($"Exception Path={exceptionDetails.Path}" + $"Exception " +
                $"Message={exceptionDetails.Error.Message}");
            return View("Error");

        }
    }
}
