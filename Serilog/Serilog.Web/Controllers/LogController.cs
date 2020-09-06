// -----------------------------------------------------------------
// <copyright>Copyright (C) 2020, David Ruiz.</copyright>
// Licensed under the Apache License, Version 2.0.
// You may not use this file except in compliance with the License:
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Software is distributed on an "AS IS", WITHOUT WARRANTIES
// OR CONDITIONS OF ANY KIND, either express or implied.
// -----------------------------------------------------------------

namespace Serilog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Serilog.Web.Models;

    public class LogController : Controller
    {
        public LogController(ILogger<LogController> logger)
        {
            logger.LogInformation("[LogController] Constructor");
            this.logger = logger;
        }

        public IActionResult Index(LogViewModel model)
        {
            logger.LogInformation("[LogController] Index");

            if (model.Message == null)
            {
                logger.LogInformation("[LogController] Index Start");
            }
            else
            {
                logger.Log(model.Level, "LOG MESSAGE: " + model.Message);
            }

            return View();
        }

        private readonly ILogger<LogController> logger;
    }
}