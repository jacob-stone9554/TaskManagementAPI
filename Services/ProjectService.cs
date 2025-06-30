using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class ProjectService
    {
        private readonly List<Project> _projects = new List<Project>();
        private readonly TaskService _taskService;
        public ProjectService(TaskService taskService) 
        {
            _taskService = taskService;
        }

        public IEnumerable<Project> GetAllProjects() => _projects;
        public IEnumerable<TaskItem> GetTasksByProject(int projectId)
        {
            var project = GetProject(projectId);

            if (project == null)
            {
                return null;
            }

            var tasks = project.tasks;
            return tasks;
        }

        public Project GetProject(int id) => _projects.FirstOrDefault(p => p.Id == id);

        public TaskItem GetTask(int projectId, int taskId)
        {
            var project = GetProject(projectId);

            if (project == null)
            {
                return null;
            }

            var task = project.tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return null;
            }

            return task;
        }

        public Project CreateProject(Project project)
        {
            project.Id = _projects.Count() + 1;
            _projects.Add(project);
            return project;
        }

        public TaskItem CreateTask(TaskItem task, int projectId)
        {
            var project = GetProject(projectId);

            if (project == null)
            {
                return null;
            }

            task.Id = project.tasks.Count() + 1;
            project.tasks.Add(task);
            return task;
        }

        public Project UpdateProject(int id, Project project)
        {
            var currProj = GetProject(id);

            if (currProj == null)
            {
                return null;
            }

            currProj.Name = project.Name;
            currProj.Description = project.Description;
            currProj.tasks = project.tasks;

            return currProj;
        }

        public TaskItem UpdateTask(TaskItem task, int projectId)
        {
            var updatedTask = _taskService.Update(task.Id, projectId, task);

            if(updatedTask == null)
            {
                return null;
            }

            return updatedTask;
        }

        public bool DeleteProject(int id)
        {
            var proj = GetProject(id);
            return proj != null && _projects.Remove(proj);
        }

        public bool DeleteTask(int projectId, int taskId)
        {
            var proj = GetProject(projectId);

            if (proj == null)
            {
                return false;
            }

            return _taskService.Delete(taskId, projectId);
        }

    }
}
