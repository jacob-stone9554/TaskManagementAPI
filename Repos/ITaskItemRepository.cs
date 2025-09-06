using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repos
{
    public interface ITaskItemRepository
    {
        public Task<List<TaskItem>?> GetTaskItemsAsync(int projectId);
        public Task<TaskItem?> GetTaskByIdAsync(int id, int projectId);
        public Task AddAsync(TaskItem item);
        public Task UpdateAsync(TaskItem item);
        public Task<bool> DeleteAsync(int id, int projectId);
    }
}
