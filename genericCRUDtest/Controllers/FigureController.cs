using genericCRUD.Models;
using genericCRUDtest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace genericCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FigureController : GenericCrudController<Figure>
    {
        protected readonly DataContext _context;
        public FigureController(DataContext context) : base(context)
        {
            _context = context;
        }
        //[HttpGet("GetAllColors")]
        //public virtual async Task<IActionResult> ListOfColors()
        //{
        //    var list = await _context.Figures
        //        .Include(c=> c.FigureColor)
        //        .ToListAsync();
        //    return Ok(list);
        //}
    }


}
