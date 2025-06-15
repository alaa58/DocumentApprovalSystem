using System.ComponentModel.DataAnnotations.Schema;
using DocumentApprovalSystemTask.Domain.Enums;

namespace DocumentApprovalSystemTask.Domain.Entities
{
    public class File: BaseModel
    {
        public string? FileNumber { get; set; }       
        public string? Subject { get; set; }        
        public FileStatus Status { get; set; }          
        public DateTime CreatedDate { get; set; }
        public string? AttachmentPath { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("SubmittedBy")]
        public string SubmittedById { get; set; }    
        public virtual Employee? SubmittedBy { get; set; }
        public virtual List<FileApproval>? Approvals { get; set; }
        public virtual FileCategory? Category { get; set; }
    }
}
