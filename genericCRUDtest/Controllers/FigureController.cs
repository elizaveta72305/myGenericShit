using genericCRUD.Models;
using genericCRUDtest.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace genericCRUDtest.Controllers
{
    [Route("api/Figure")]
    [ApiController]
    public class FigureController : ControllerBase
    {
        private readonly IRepository<Figure> _repository;
        private readonly DataContext _dataContext;
        public FigureController(IRepository<Figure> repository, DataContext dataContext)
        {
            _repository = repository;
            _dataContext = dataContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var figureCoolr = _dataContext.Figures.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Figure entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Figure entity)
        {
            if (id != entity.Id)
                return BadRequest();

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            await _repository.DeleteAsync(entity);

            return NoContent();
        }
    }
}
