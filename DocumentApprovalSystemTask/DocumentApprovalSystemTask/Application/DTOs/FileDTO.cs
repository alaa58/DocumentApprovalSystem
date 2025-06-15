namespace DocumentApprovalSystemTask.Application.DTOs
{
    public class FileDTO
    {
        public int Id { get; set; }
        public string? FileNumber { get; set; }
        public string? Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } 
        public string? SubmittedByName { get; set; }
        public string? CategoryName { get; set; }
    }
}
