using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repos
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>?> GetTaskItemsAsync(int projectId)
        {
            return await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id, int projectId)
        {
            return await _context.Tasks
                .Where(t => t.Id == id && t.ProjectId == projectId)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(TaskItem item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem item)
        {
            _context.Tasks.Update(item);
            await _context.SaveChangesAsync();
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
