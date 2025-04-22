using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly IExpressVoituresService _service;

        public CarController(IExpressVoituresService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int id)
        {
            var voitureProfileModel = await _service.GetCarAsync(id);
            if (voitureProfileModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(voitureProfileModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var newVoitureProfileModel = await _service.GetNewVoitureProfileModel();
            return View(newVoitureProfileModel);
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
