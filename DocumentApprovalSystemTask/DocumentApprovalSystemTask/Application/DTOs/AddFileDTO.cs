using System.ComponentModel.DataAnnotations.Schema;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Enums;

namespace DocumentApprovalSystemTask.Application.DTOs
{
    public class AddFileDTO
    {
        public int Id { get; set; } 
        public string? FileNumber { get; set; }
        public string? Subject { get; set; }
        public FileStatus Status { get; set; } = FileStatus.Pending;
        public DateTime CreatedDate { get; set; }
        public string? AttachmentPath { get; set; }
        public int CategoryId { get; set; }
        public List<string> ResponsibleEmployeeIds { get; set; } = new();

    }
}
