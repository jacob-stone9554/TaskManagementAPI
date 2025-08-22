namespace TaskManagementAPI.DTOs
{
    public class TaskItemReadDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public bool Completed { get; set; }
    }
}
