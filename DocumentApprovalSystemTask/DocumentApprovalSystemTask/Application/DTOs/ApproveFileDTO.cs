using DocumentApprovalSystemTask.Domain.Enums;

namespace DocumentApprovalSystemTask.Application.DTOs
{
    public class ApproveFileDTO
    {
        public int FileId { get; set; }
        public int ApprovalOrder { get; set; }
        public FileStatus Status { get; set; }
    }
}
