using System.Diagnostics;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using ExpressVoitures.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExpressVoituresService _service;

        public HomeController(ILogger<HomeController> logger, IExpressVoituresService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            List<VoitureModel> listAllCars = _service.GetAllCars().Result;
            return View(listAllCars);
        }

        public IActionResult CreateNew()
        {
            return View();
        }

        public IActionResult Create(VoitureModel model)
        {
            var a = _service.CreateVoitureAsync(model);
            return RedirectToAction("Index");
        }

        public IActionResult Update()
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
