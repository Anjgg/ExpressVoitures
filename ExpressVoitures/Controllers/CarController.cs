using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers
{
    public class CarController : Controller
    {
        private readonly IExpressVoituresService _service;

        public CarController(IExpressVoituresService service)
        {
            _service = service;
        }

        //    [HttpGet]
        //    public async Task<IActionResult> Index(string codeVin)
        //    {
        //        if (codeVin.IsNullOrEmpty())
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        var voiture = await _service.GetCarAsync(codeVin);
        //        if (voiture == null)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        return View(voiture);
        //    }

        //    [HttpGet]
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Create(VoitureModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }
        //        var voiture = await _service.CreateVoitureAsync(model);
        //        return RedirectToAction("CarCreated", voiture);
        //    }

        //    [HttpGet]
        //    public IActionResult CarCreated(VoitureModel model)
        //    {
        //        return View(model);
        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> Update(string codeVin)
        //    {
        //        var voiture = await _service.GetCarAsync(codeVin);
        //        return View(voiture);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Update(VoitureModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }
        //        var voiture = await _service.UpdateCarAsync(model);
        //        return RedirectToAction("CarUpdated", voiture);
        //    }

        //    [HttpGet]
        //    public IActionResult CarUpdated(VoitureModel model)
        //    {
        //        return View(model);
        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> Delete(string codeVin)
        //    {
        //        var voiture = await _service.DeleteCarAsync(codeVin);
        //        return View(voiture);
        //    }
    }
}
