using ExpressVoitures.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers
{
    public class CarController : Controller
    {

        public IActionResult Create()
        {
            
            return View();
        }

        
    }
}
