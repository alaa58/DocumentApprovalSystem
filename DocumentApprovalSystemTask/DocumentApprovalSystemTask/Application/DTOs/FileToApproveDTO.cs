namespace DocumentApprovalSystemTask.Application.DTOs
{
    public class FileToApproveDTO
    {
        public int FileId { get; set; }
        public string FileNumber { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ApprovalOrder { get; set; }
        public string Status { get; set; }
    }
}
