using genericCRUD.Controllers;
using genericCRUD.Models;
using genericCRUDtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace genericCRUDtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : GenericCrudController<Color>
    {
        protected readonly DataContext _context;
        public ColorController(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
