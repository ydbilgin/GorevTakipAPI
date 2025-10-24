using Microsoft.AspNetCore.Mvc;
using GorevTakipAPI.Application.DTOs;
using GorevTakipAPI.Application.Services;

namespace GorevTakipAPI.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _svc;
        public TasksController(ITaskService svc) => _svc = svc;

        [HttpGet]
        public IActionResult GetAll() => Ok(_svc.GetAll());

        [HttpPost]
        public IActionResult Create(TaskDto dto)
        {
            var result = _svc.Add(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskDto dto)
        {
            var result = _svc.Update(id, dto);
            return Ok(result);
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            _svc.Complete(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _svc.Remove(id);
            return NoContent();
        }
    }
}
