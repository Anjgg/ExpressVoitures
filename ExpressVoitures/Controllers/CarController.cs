using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Controllers
{
    public class CarController : Controller
    {
        private readonly IExpressVoituresService _service;

        public CarController(IExpressVoituresService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var voiture = await _service.GetCarAsync(id);
            if (voiture == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(voiture);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VoitureProfileModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoitureProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var voiture = await _service.CreateVoitureAsync(model);
            return RedirectToAction("CarCreated", new { marque = voiture.Voiture.Marque, modele = voiture.Voiture.Modele});
        }

        [HttpGet]
        public IActionResult CarCreated(string marque, string modele)
        {
            ViewBag.Marque = marque;
            ViewBag.Modele = modele;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var voiture = await _service.GetCarAsync(id);

            var listTypeModel = await _service.GetListTypeModel();
            ViewBag.Types = new SelectList(listTypeModel, "Id", "Description");

            return View(voiture);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VoitureProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var voiture = await _service.UpdateCarAsync(model);

            

            return RedirectToAction("CarUpdated", new { marque = voiture.Voiture.Marque, modele = voiture.Voiture.Modele });
        }

        [HttpGet]
        public IActionResult CarUpdated(string marque, string modele)
        {
            ViewBag.Marque = marque;
            ViewBag.Modele = modele;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var voiture = await _service.DeleteCarAsync(id);
            return View(voiture);
        }
    }
}
