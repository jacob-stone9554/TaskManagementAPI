using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repos
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Project>?> GetProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var projToDelete = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (projToDelete != null)
            {
                _context.Projects.Remove(projToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
