using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Services
{
    public class ProjectService
    {        
        private readonly AppDbContext _context;
        
        public ProjectService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync() 
        {
            return await _context.Projects.Include(p => p.tasks).ToListAsync();
        } 

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectAsync(int projectId)
        {
            return await _context.Projects.Where(p => p.Id == projectId)
                .SelectMany(p => p.tasks)
                .ToListAsync();
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await _context.Projects.Include(p => p.tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }                        

        public async Task<ProjectReadDTO> CreateProjectAsync(ProjectCreateDTO project)
        {            
            var newProj = new Project
            {
                Name = project.Name,
                Description = project.Description
            };

            await _context.AddAsync(project);
            await _context.SaveChangesAsync();

            ProjectReadDTO projectReadDTO = new ProjectReadDTO();
            //projectReadDTO.Id

            return projectReadDTO;
        }
        
        public async Task<Project> UpdateProjectAsync(int id, Project project)
        {
            var currProj = await GetProjectAsync(id);

            if (currProj == null)
            {
                return null;
            }

            currProj.Name = project.Name;
            currProj.Description = project.Description;
            currProj.tasks = project.tasks;

            await _context.SaveChangesAsync();

            return currProj;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var projToDelete = await GetProjectAsync(id);

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
