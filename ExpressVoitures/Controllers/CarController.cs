using ExpressVoitures.Data.Models;
using ExpressVoitures.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExpressVoitures.Controllers
{
    public class CarController : Controller
    {
        private readonly IExpressVoituresService _service;

        public CarController(IExpressVoituresService service)
        {
            _service = service;
        }

        public IActionResult ShowCar(string carId)
        {
            return View();
        }
        public IActionResult CreateCar(VoitureModel model)
        {
            if (model.CodeVin.IsNullOrEmpty())
            {
                return View();
            }
            var voiture = _service.CreateVoitureAsync(model).Result;
            return View("GetCar", voiture);
        }

        public IActionResult GetCar(VoitureModel model)
        {
            if (model.CodeVin == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
