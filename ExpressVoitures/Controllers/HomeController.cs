using ExpressVoitures.Data.Models;
using ExpressVoitures.Services;
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
