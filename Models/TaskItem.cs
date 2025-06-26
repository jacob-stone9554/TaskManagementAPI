namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Boolean completed { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
