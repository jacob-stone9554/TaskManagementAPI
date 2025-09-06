using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Repos;

namespace TaskManagementAPI.Services
{
    public class TaskService
    {
        private readonly TaskItemRepository _taskRepo;
        private readonly ProjectRepository _projectRepo;

        public TaskService(ProjectRepository projectRepo, TaskItemRepository taskItemRepo) 
        {                    
            _projectRepo = projectRepo;
            _taskRepo = taskItemRepo;
        }

        public async Task<IEnumerable<TaskItemReadDTO>> GetAllAsync(int projectId)
        {
            var items = await _taskRepo.GetTaskItemsAsync(projectId);

            return items.Select(t => new TaskItemReadDTO
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                Name = t.Name,
                Description = t.Description,
                Completed = t.completed,
                Priority = t.Priority
            });
        }

        public async Task<TaskItemReadDTO> GetAsync(int id, int projectId)
        {
            var task = await _taskRepo.GetTaskByIdAsync(id, projectId);                

            return new TaskItemReadDTO()
            {
                ProjectId = task.ProjectId,
                Name = task.Name,
                Description = task.Description,
                Completed = task.completed,
                Priority = task.Priority
            };            
        }

        public async Task<TaskItemReadDTO> CreateAsync(int projectId, TaskItemCreateDTO item)
        {
            var project = await _projectRepo.GetByIdAsync(projectId);

            if (project == null)
                return null;

            var task = new TaskItem()
            {
                ProjectId = projectId,
                Name = item.Name,
                Description = item.Description,
                Priority = item.Priority
            };           

            await _taskRepo.AddAsync(task);

            return new TaskItemReadDTO()
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Name = task.Name,
                Description = task.Description,
                Completed = task.completed,
                Priority = task.Priority
            };            
        }
        public async Task<TaskItemReadDTO> UpdateAsync(int id, int projectId, TaskItemUpdateDTO item)
        {
            var currTask = await _taskRepo.GetTaskByIdAsync(id, projectId);

            if(currTask == null)
            {
                return null;
            }

            currTask.Name = item.Name;
            currTask.Description = item.Description;
            currTask.Priority = item.Priority;
            currTask.completed = item.Completed;

            await _taskRepo.UpdateAsync(currTask);

            return new TaskItemReadDTO()
            {
                ProjectId = currTask.ProjectId,
                Name = currTask.Name,
                Description = currTask.Description,
                Priority = currTask.Priority,
                Completed = currTask.completed
            };
        }

        public async Task<bool> DeleteAsync(int id, int projectId)
        {
            return await _taskRepo.DeleteAsync(id, projectId);
        }
    }
}
