﻿using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var newVoitureProfileModel = await _service.GetNewVoitureProfileModelAsync();
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
            return RedirectToAction("CarCreated");
        }

        [HttpGet]
        public IActionResult CarCreated()
        {
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
            
            return RedirectToAction("CarUpdated");
        }

        [HttpGet]
        public IActionResult CarUpdated()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteCarAsync(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeleteError", $"Erreur lors de la suppression de la voiture : {ex.Message}");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", id);
            }
            return View();
        }
    }
}
