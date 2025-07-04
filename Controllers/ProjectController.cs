﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("/api/projects/{projectId}/tasks")]
        public IActionResult GetTasksByProject(int projectId)
        {
            //make sure the project exists
            var project = _projectService.GetProject(projectId);

            if (project == null)
                return NotFound(new { Message = $"No project found for a Project ID of {projectId}" });


            var tasks = _taskService.GetAll(projectId);

            if(tasks == null)
            {
                return NotFound(new { Message = $"No tasks found for a Project ID of {projectId}" });
            }

            return Ok(tasks);
        }

        [HttpGet("/api/projects/{projectId}/tasks/{taskId}")]
        public IActionResult GetTaskById(int projectId, int taskId)
        {
            var task = _taskService.Get(taskId, projectId);

            if (task == null)
            {
                return NotFound(new { Message = $"No task found with ID of {taskId} in Project with ID of {projectId}" });
            }

            return Ok(task);
        }

        [HttpPost("/api/projects")]
        public IActionResult CreateProject([FromBody] Project project)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newProject = _projectService.CreateProject(project);
            return CreatedAtAction(nameof(GetProject), new { projectId = project.Id }, newProject);
        }

        [HttpPost("/api/projects/{projectId}/tasks")]
        public IActionResult CreateTask(int projectId, [FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newTask = _taskService.Create(projectId, task);

            if (newTask == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetTaskById), new { projectId = projectId, taskId = newTask.Id }, newTask);
        }

        [HttpPatch("/api/projects/{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] Project project)
        {
            if(! ModelState.IsValid) return BadRequest();

            var updatedProj = _projectService.UpdateProject(projectId, project);

            if (updatedProj == null)
                return NotFound(new { Message = $"No project found with ID of {projectId}" });

            return Ok(updatedProj);
        }

        [HttpPatch("/api/projects/{projectId}/tasks/{taskId}")]
        public IActionResult UpdateTask(int projectId, int taskId, [FromBody] TaskItem task)
        {
            if (!ModelState.IsValid) return BadRequest();

            var updatedTask = _taskService.Update(taskId, projectId, task);

            if (updatedTask == null)
                return NotFound(new { Message = $"No task found with ID of {taskId} for project with ID of {projectId}" });

            return Ok(updatedTask);
        }

        [HttpDelete("/api/projects/{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            var project = _projectService.GetProject(projectId);

            if(project == null) 
                return NotFound(new { Message = $"No project found with ID of {projectId}" });

            bool success = false;

            //delete project tasks to prevent orphans
            foreach(var task in project.tasks)
            {
                success = _taskService.Delete(task.Id, projectId);
            }

            success = _projectService.DeleteProject(projectId);

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
        public IActionResult DeleteTask(int projectId, int taskId)
        {
            var task = _taskService.Get(taskId, projectId);

            if (task == null)
                return NotFound(new { Message = $"No task found with ID of {taskId} for project with ID of {projectId}" });

            bool success = _taskService.Delete(task.Id, projectId);

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
