using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetItems();
        TaskItem GetItem(int id);
        TaskItem Create(TaskItem item);
        TaskItem Update(int id, TaskItem item);
        bool Delete(int id);
    }
}
