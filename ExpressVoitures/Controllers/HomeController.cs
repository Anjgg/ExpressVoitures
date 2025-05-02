using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpressVoituresService _service;

        public HomeController(IExpressVoituresService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<HomeCarModel> listAllHomeCars = await _service.GetAllHomeCars();
            return View(listAllHomeCars);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
