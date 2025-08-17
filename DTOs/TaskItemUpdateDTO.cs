namespace TaskManagementAPI.DTOs
{
    public class TaskItemUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool Completed { get; set; }
    }
}
