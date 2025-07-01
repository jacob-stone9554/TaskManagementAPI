using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class ProjectService
    {
        private readonly List<Project> _projects = new List<Project>();
        
        public ProjectService() 
        {
            
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

        public Project CreateProject(Project project)
        {
            project.Id = _projects.Count() + 1;
            _projects.Add(project);
            return project;
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

        public bool DeleteProject(int id)
        {
            var proj = GetProject(id);
            return proj != null && _projects.Remove(proj);
        }
    }
}
