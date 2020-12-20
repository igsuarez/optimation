﻿using Microsoft.AspNetCore.Mvc;

namespace ProcessingService.Api.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
