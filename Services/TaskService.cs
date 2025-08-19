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
                    ProjectId = t.ProjectId,
                    Name = t.Name,
                    Description = t.Description,
                    Completed = t.completed,
                    Priority = t.Priority
                })
                .ToListAsync();
        }

        public async Task<TaskItem> GetAsync(int id, int projectId)
        {
            return await _context.Tasks.Where(t => t.ProjectId == projectId && t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TaskItem> CreateAsync(int projectId, TaskItem item)
        {
            var project = await _projectService.GetProjectAsync(projectId);

            if (project == null)
                return null;

            item.ProjectId = project.Id;

            await _context.Tasks.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<TaskItem> UpdateAsync(int id, int projectId, TaskItem item)
        {
            var currTask = await GetAsync(id, projectId);

            if(currTask == null)
            {
                return null;
            }

            currTask.Name = item.Name;
            currTask.Description = item.Description;
            currTask.Priority = item.Priority;
            currTask.completed = item.completed;
            currTask.IsDeleted = item.IsDeleted;

            await _context.SaveChangesAsync();

            return currTask;
        }

        public async Task<bool> DeleteAsync(int id, int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);

            if (project == null)
                return false;

            var task = await GetAsync(id, projectId);

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
