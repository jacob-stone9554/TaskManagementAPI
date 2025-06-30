using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        public TaskService() { }

        public IEnumerable<TaskItem> GetAll(int projectId)
        {
            var tasks = _tasks.Where(t => t.ProjectId == projectId);

            return tasks;
        }

        public TaskItem Get(int id, int projectId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id && t.ProjectId == projectId);

            return task;
        }

        public TaskItem Create(TaskItem item)
        {
            item.Id = _tasks.Count + 1;
            _tasks.Add(item);
            return item;
        }
        public TaskItem Update(int id, int projectId, TaskItem item)
        {
            var currTask = Get(id, projectId);

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

        public bool Delete(int id, int projectId)
        {
            var task = Get(id, projectId);
            return task != null && _tasks.Remove(task);
        }

    }
}
