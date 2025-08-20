using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs
{
    public class TaskItemCreateDTO
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }
}
