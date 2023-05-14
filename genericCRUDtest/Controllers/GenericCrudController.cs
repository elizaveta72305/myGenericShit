using genericCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace genericCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericCrudController<T> : ControllerBase where T : EntitiyBase
    {
        protected readonly DataContext _context;

        public GenericCrudController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var navigation in _context.Model.FindEntityType(typeof(T))!.GetNavigations())
            {
                query = query.Include(navigation.Name);
            }
            var list = await query.ToListAsync();
            return Ok(list);

            //    if (typeof(T) == typeof(Figure))
            //    {
            //        var list = await _context.Figures.Include(c => c.FigureColor).ToListAsync();
            //        return Ok(list);
            //    }
            //    else
            //    {
            //        var list = await _context.Set<T>().ToListAsync();
            //        return Ok(list);
            //    }
            //}
        }

            [HttpGet("{id}")]
        public virtual async Task<IActionResult> Detail(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var entityType = _context.Model.FindEntityType(typeof(T));
            var includeStrings = new List<string>();
            foreach (var navigation in entityType!.GetNavigations())
            {
                includeStrings.Add(navigation.Name);
            }
            //foreach (var navigation in entityType.GetNavigations())
            //{
            //    includeStrings.Add(navigation.Name);
            //}

            var query = _context.Set<T>().AsQueryable();
            foreach (var includeString in includeStrings)
            {
                query = query.Include(includeString);
            }

            entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            return Ok(entity);

            //if (typeof(T) == typeof(Figure))
            //{
            //    var list = await _context.Figures.Include(c => c.FigureColor).FirstOrDefaultAsync(c => c.Id == id);
            //    return Ok(list);
            //}
            //else
            //{
            //    var entitiy = await _context.Set<T>().FindAsync(id);
            //    if (entitiy == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(entitiy);
            //}
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(T entity)
        {

            entity.EntryDate = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Detail", new { id = entity.Id }, entity);
        }

        [HttpPut]

        public virtual async Task<IActionResult> Update(long id, T entity)
        {
            if(id != entity.Id)
            {
                return BadRequest();
            }
            if(!await EntityExist(id))
            {
                return NotFound();
            }
            entity.UpdateDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private Task<bool> EntityExist(long id)
        {
            return _context.Set<T>().AnyAsync(e=> e.Id == id);
        }
    }
}
