using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class ProjectService : IServiceBase<Project>
    {
        private readonly List<Project> _projects = new List<Project>();
        public ProjectService() { }

        public IEnumerable<Project> GetAll() => _projects;

        public Project Get(int id) => _projects.FirstOrDefault(p => p.Id == id);

        public Project Create(Project project)
        {
            project.Id = _projects.Count() + 1;
            _projects.Add(project);
            return project;
        }

        public Project Update(int id, Project project)
        {
            var currProj = Get(id);

            if (currProj == null)
            {
                return null;
            }

            currProj.Name = project.Name;
            currProj.Description = project.Description;
            currProj.tasks = project.tasks;

            return currProj;
        }

        public bool Delete(int id)
        {
            var proj = Get(id);
            return proj != null && _projects.Remove(proj);
        }

    }
}
