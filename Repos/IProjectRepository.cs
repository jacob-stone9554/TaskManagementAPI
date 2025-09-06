using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repos
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(int id);
        Task<List<Project>?> GetProjectsAsync();
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
    }
}
