using genericCRUD.Models;
using genericCRUDtest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using genericCRUDtest.Models;
using genericCRUDtest.Repository;

namespace genericCRUD.Controllers
{
    [Route("api/Colors")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IRepository<Color> _repository;

        public ColorController(IRepository<Color> repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(Color entity)
        {
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Color entity)
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
