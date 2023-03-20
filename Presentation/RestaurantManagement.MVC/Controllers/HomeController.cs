using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.Interfaces;
using RestaurantManagement.Application;
using RestaurantManagement.Infrastructure.Services;
using RestaurantManagement.MVC.Models;
using System.Diagnostics;

namespace RestaurantManagement.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceOfWork _service;

        public HomeController(ILogger<HomeController> logger, IServiceOfWork service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Deneme";

            //var answer = await _service.OpenAI.ImageGeneratorAsync("dolphin with human face");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Chat(string question)
        {
            var answer = await _service.OpenAI.CompletionAsync(question);
            if (!string.IsNullOrEmpty(answer))
            {
                return Ok(answer.Trim());
            }
            else
            {
                return BadRequest(question);
            }

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