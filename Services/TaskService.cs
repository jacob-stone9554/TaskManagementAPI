using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskService : IServiceBase<TaskItem>
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        public TaskService() { }

        public IEnumerable<TaskItem> GetAll() => _tasks;
        public TaskItem Get(int id) => _tasks.FirstOrDefault(t => t.Id == id);
        public TaskItem Create(TaskItem item)
        {
            item.Id = _tasks.Count + 1;
            _tasks.Add(item);
            return item;
        }
        public TaskItem Update(int id, TaskItem item)
        {
            var currTask = Get(id);

            if(currTask == null)
            {
                return null;
            }

            currTask.Name = item.Name;
            currTask.Description = item.Description;
            currTask.Priority = item.Priority;
            currTask.completed = item.completed;
            currTask.IsDeleted = item.IsDeleted;

            return currTask;
        }

        public bool Delete(int id)
        {
            var task = Get(id);
            return task != null && _tasks.Remove(task);
        }

    }
}
