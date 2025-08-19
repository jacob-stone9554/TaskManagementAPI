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

        public async Task<IEnumerable<ProjectReadDTO>> GetAllProjectsAsync() 
        {
            var projects = await _context.Projects.ToListAsync();

            var projectDTOs = projects.Select(p => new ProjectReadDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            });

            //return await _context.Projects.Include(p => p.tasks).ToListAsync();
            return projectDTOs;
        } 

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectAsync(int projectId)
        {
            return await _context.Projects.Where(p => p.Id == projectId)
                .SelectMany(p => p.tasks)
                .ToListAsync();
        }

        public async Task<ProjectReadDTO> GetProjectAsync(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            var projectReadDTO = new ProjectReadDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            };

            return projectReadDTO;
        }                        

        public async Task<ProjectReadDTO> CreateProjectAsync(ProjectCreateDTO project)
        {            
            var newProj = new Project
            {
                Name = project.Name,
                Description = project.Description
            };

            await _context.AddAsync(newProj);
            await _context.SaveChangesAsync();

            ProjectReadDTO projectReadDTO = new ProjectReadDTO()
            {
                Id = newProj.Id,
                Name = project.Name,
                Description = project.Description
            };
            return projectReadDTO;
        }
        
        public async Task<ProjectReadDTO> UpdateProjectAsync(int id, ProjectUpdateDTO project)
        {
            var currProj = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (currProj == null)
            {
                return null;
            }

            currProj.Name = project.Name;
            currProj.Description = project.Description;
            
            await _context.SaveChangesAsync();

            var readDTO = new ProjectReadDTO()
            {
                Id = currProj.Id,
                Name = currProj.Name,
                Description = currProj.Description
            };

            return readDTO;
        }

        public async Task<bool> DeleteProjectAsync(int id)
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
