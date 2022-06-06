using ifunanyaonah.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ifunanyaonah.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger,
                              EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmailViewModel model)
        {
            if (!ModelState.IsValid) { return View(model);  }
                
            await _emailService.SendMail(model);
            return RedirectToAction("Sent");
        }

        public IActionResult Sent()
        {
            TempData["Success"] = "Email sent successfully";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
