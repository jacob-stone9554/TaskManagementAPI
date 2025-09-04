using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;
        private readonly ProjectService _projectService;

        public TaskService(ProjectService projectService, AppDbContext context) 
        {
            _projectService = projectService;
            _context = context;
        }

        public async Task<IEnumerable<TaskItemReadDTO>> GetAllAsync(int projectId)
        {
            return await _context.Tasks.Where(t => t.ProjectId == projectId)
                .Select(t => new TaskItemReadDTO
                {
                    Id = t.Id,
                    ProjectId = t.ProjectId,
                    Name = t.Name,
                    Description = t.Description,
                    Completed = t.completed,
                    Priority = t.Priority
                })
                .ToListAsync();
        }

        public async Task<TaskItemReadDTO> GetAsync(int id, int projectId)
        {
            var task = await _context.Tasks.Where(t => t.ProjectId == projectId && t.Id == id)
                .FirstOrDefaultAsync();

            var taskReadDTO = new TaskItemReadDTO()
            {
                ProjectId = task.ProjectId,
                Name = task.Name,
                Description = task.Description,
                Completed = task.completed,
                Priority = task.Priority
            };

            return taskReadDTO;
        }

        //public async Task<ProjectReadDTO> CreateProjectAsync(ProjectCreateDTO project)
        //{
        //    var newProj = new Project
        //    {
        //        Name = project.Name,
        //        Description = project.Description
        //    };

        //    await _context.AddAsync(newProj);
        //    await _context.SaveChangesAsync();

        //    ProjectReadDTO projectReadDTO = new ProjectReadDTO()
        //    {
        //        Id = newProj.Id,
        //        Name = project.Name,
        //        Description = project.Description
        //    };
        //    return projectReadDTO;
        //}

        public async Task<TaskItemReadDTO> CreateAsync(int projectId, TaskItemCreateDTO item)
        {
            var project = await _projectService.GetProjectAsync(projectId);

            if (project == null)
                return null;

            var task = new TaskItem()
            {
                ProjectId = projectId,
                Name = item.Name,
                Description = item.Description,
                Priority = item.Priority
            };

            //item.ProjectId = project.Id;

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            var taskItemReadDTO = new TaskItemReadDTO()
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Name = task.Name,
                Description = task.Description,
                Completed = task.completed,
                Priority = task.Priority
            };

            return taskItemReadDTO;
        }
        public async Task<TaskItemReadDTO> UpdateAsync(int id, int projectId, TaskItemUpdateDTO item)
        {
            var currTask = await _context.Tasks.Where(t => t.Id == id && t.ProjectId == projectId)
                .FirstOrDefaultAsync();

            if(currTask == null)
            {
                return null;
            }

            currTask.Name = item.Name;
            currTask.Description = item.Description;
            currTask.Priority = item.Priority;
            currTask.completed = item.Completed;

            await _context.SaveChangesAsync();

            var taskReadDTO = new TaskItemReadDTO()
            {
                ProjectId = currTask.ProjectId,
                Name = currTask.Name,
                Description = currTask.Description,
                Priority = currTask.Priority,
                Completed = currTask.completed
            };

            return taskReadDTO;
        }

        public async Task<bool> DeleteAsync(int id, int projectId)
        {
            var task = await _context.Tasks.Where(t => t.Id == id && t.ProjectId == projectId)
                .FirstOrDefaultAsync();

            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
