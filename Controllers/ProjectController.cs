using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

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
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAllProjects();

            if (!projects.Any())
            {
                return NotFound(new { Message = "No projects found." });
            }

            return Ok(projects);
        }

        [HttpGet("/api/projects/{projectId}")]
        public IActionResult GetProject(int projectId)
        {
            var project = _projectService.GetProject(projectId);

            if (project == null)
            {
                return NotFound(new { Message = $"No projects found with ID of {projectId}."});
            }

            return Ok(project);
        }

        [HttpGet]
        public IActionResult GetTasksByProject(int projectId)
        {
            return Ok();
        }

    }
}
