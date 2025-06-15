using System.ComponentModel.DataAnnotations.Schema;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Enums;

namespace DocumentApprovalSystemTask.Application.DTOs
{
    public class AddFileApprovalDTO 
    {
        public int ApprovalOrder { get; set; }
        public FileStatus Status { get; set; } = FileStatus.Pending;
        public DateTime? DecisionDate { get; set; }
        public int FileId { get; set; }
        public List<string>? ResponsibleEmployeeIds { get; set; }
    }
}
