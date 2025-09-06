using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Repos;

namespace TaskManagementAPI.Services
{
    public class ProjectService
    {        
        private readonly AppDbContext _context; // remove this after updating to use ProjectRepo
        private readonly ProjectRepository _projectRepo;

        
        public ProjectService(AppDbContext context, ProjectRepository projectRepo) 
        {
            _context = context;
            _projectRepo = projectRepo;
        }

        public async Task<IEnumerable<ProjectReadDTO>> GetAllProjectsAsync() 
        {
            var projects = await _projectRepo.GetProjectsAsync();

            var projectDTOs = projects.Select(p => new ProjectReadDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            });
            
            return projectDTOs;
        } 

        public async Task<IEnumerable<TaskItemReadDTO>> GetTasksByProjectAsync(int projectId)
        {
            return await _context.Projects.Where(p => p.Id == projectId)
                .SelectMany(p => p.tasks)
                .Select(t => new TaskItemReadDTO
                {
                    ProjectId = t.ProjectId,
                    Name = t.Name,
                    Description = t.Description,
                    Priority = t.Priority,
                    Completed = t.completed
                })
                .ToListAsync();
        }

        public async Task<ProjectReadDTO> GetProjectAsync(int id)
        {
            var project = await _projectRepo.GetByIdAsync(id);

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

            await _projectRepo.AddAsync(newProj);

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
            var currProj = await _projectRepo.GetByIdAsync(id);

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
            return await _projectRepo.DeleteAsync(id);
        }
    }
}
