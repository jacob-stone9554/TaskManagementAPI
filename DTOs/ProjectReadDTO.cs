using TaskManagementAPI.Models;

namespace TaskManagementAPI.DTOs
{
    public class ProjectReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
