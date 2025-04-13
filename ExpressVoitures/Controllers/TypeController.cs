using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers
{
    [Route("api/[controller]")]
    public class TypeController : Controller
    {
        public readonly ExpressVoituresContext _context;

        public TypeController(ExpressVoituresContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDto>> GetReparation(int id)
        {
            var typeDto = await _context.Types.FindAsync(id);
            if (typeDto == null)
            {
                return NotFound();
            }
            var typeModel = new TypeModel
            {
                Id = typeDto.Id,
                Description = typeDto.Description,
                Prix = typeDto.Prix,
                Duree = typeDto.Duree
            };

            return Ok(typeModel);
        }
    }
}
