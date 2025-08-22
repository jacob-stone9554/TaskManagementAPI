using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
        
        public ProjectController(ProjectService projectService, TaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        [HttpGet("/api/projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();

            if (!projects.Any())
            {
                return NotFound(new { Message = "No projects found." });
            }

            return Ok(projects);
        }

        [HttpGet("/api/projects/{projectId}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);

            if (project == null)
            {
                return NotFound(new { Message = $"No projects found with ID of {projectId}."});
            }

            return Ok(project);
        }

        [HttpGet("/api/projects/{projectId}/tasks")]
        public async Task<IActionResult> GetTasksByProject(int projectId)
        {           
            var tasks = await _taskService.GetAllAsync(projectId);

            if(tasks == null)
            {
                return NotFound(new { Message = $"No tasks found for a Project ID of {projectId}" });
            }

            return Ok(tasks);
        }

        [HttpGet("/api/projects/{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> GetTaskById(int projectId, int taskId)
        {
            var task = await _taskService.GetAsync(taskId, projectId);

            if (task == null)
            {
                return NotFound(new { Message = $"No task found with ID of {taskId} in Project with ID of {projectId}" });
            }

            return Ok(task);
        }

        [HttpPost("/api/projects")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDTO project)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newProject = await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProject), new { projectId = newProject.Id }, newProject);
        }

        [HttpPost("/api/projects/{projectId}/tasks")]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] TaskItemCreateDTO task)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newTask = await _taskService.CreateAsync(projectId, task);

            if (newTask == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetTaskById), new { projectId = projectId, taskId = newTask.Id }, newTask);
        }

        [HttpPatch("/api/projects/{projectId}")]
        public async Task<IActionResult> UpdateProject(int projectId, [FromBody] ProjectUpdateDTO project)
        {
            if(! ModelState.IsValid) return BadRequest();

            var updatedProj = await _projectService.UpdateProjectAsync(projectId, project);

            if (updatedProj == null)
                return NotFound(new { Message = $"No project found with ID of {projectId}" });

            return Ok(updatedProj);
        }

        [HttpPatch("/api/projects/{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask(int projectId, int taskId, [FromBody] TaskItemUpdateDTO task)
        {
            if (!ModelState.IsValid) return BadRequest();

            var updatedTask = await _taskService.UpdateAsync(taskId, projectId, task);

            if (updatedTask == null)
                return NotFound(new { Message = $"No task found with ID of {taskId} for project with ID of {projectId}" });

            return Ok(updatedTask);
        }

        [HttpDelete("/api/projects/{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);

            if(project == null) 
                return NotFound(new { Message = $"No project found with ID of {projectId}" });

            bool success = false;

            success = await _projectService.DeleteProjectAsync(projectId);

            if(success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("/api/projects/{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(int projectId, int taskId)
        {
            var task = await _taskService.GetAsync(taskId, projectId);

            if (task == null)
                return NotFound(new { Message = $"No task found with ID of {taskId} for project with ID of {projectId}" });

            bool success = await _taskService.DeleteAsync(taskId, projectId);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
