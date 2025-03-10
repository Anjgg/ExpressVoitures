using System.Diagnostics;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using ExpressVoitures.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        public IActionResult ShowOneCar(string codeVin)
        {
            if (codeVin.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }
            var voiture = _service.GetVoitureAsync(codeVin).Result;
            if (voiture == null)
            {
                return RedirectToAction("Index");
            }
            return View(voiture);
        }

        public IActionResult Update()
        {
            return View();
        }

        
    }
}
