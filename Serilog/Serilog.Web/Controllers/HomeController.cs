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
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Serilog.Web.Models;

    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            logger.LogInformation("[HomeController] Constructor");
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("[HomeController] Index");
            return View();
        }

        public IActionResult Privacy()
        {
            logger.LogInformation("[HomeController] Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            logger.LogError("[HomeController] Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly ILogger<HomeController> logger;
    }
}