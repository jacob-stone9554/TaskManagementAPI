using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskService.GetItems());
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _taskService.GetItem(id);

            if (task == null)
            {
                return NotFound(new { Message = $"Task with ID {id} was not found." });
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            var created = _taskService.Create(task);

            return CreatedAtAction(nameof(GetAllTasks), new { id = created.Id }, created);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem task)
        {
            var currTask = _taskService.Update(id, task);

            if (currTask == null)
            {
                return NotFound(new { Message = "$Task with ID {id} was not found." });
            }

            return Ok(currTask);
        }


        [HttpDelete]
        public IActionResult DeleteTaskById(int id)
        {
            bool deleteSuccess = _taskService.Delete(id);

            if (!deleteSuccess)
                return BadRequest(new { Message = $"There was an issue deleting task with ID of {id}." });

            return NoContent();
        }
    }
}
