﻿using ExpressVoitures.Data.Models;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Read(string codeVin)
        {
            var voiture = _service.GetVoitureAsync(codeVin).Result;
            return View("Index", voiture);
        }

        public IActionResult Create(VoitureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var voiture = _service.CreateVoitureAsync(model).Result;
            return View(model);
        }

        public IActionResult Update(VoitureModel model)
        {
            (var isModified, string errorMessage) = _service.CheckIfModified(model).Result;

            if (errorMessage != string.Empty)
            {
                ModelState.AddModelError("CodeVin", errorMessage);
            }

            if (isModified && ModelState.IsValid)
            {
                var voiture = _service.UpdateVoitureAsync(model).Result;
                return View(voiture);
            } 
            else
            {
                ViewBag.isModified = false;
                return View("Index", model);
            }
        }

        public IActionResult Delete(string? codeVin)
        {
            if (string.IsNullOrEmpty(codeVin))
            {
                return RedirectToAction("Index", "Home");
            }
            var car = _service.DeleteVoitureAsync(codeVin).Result;
            return View(car);
        }
    }
}
