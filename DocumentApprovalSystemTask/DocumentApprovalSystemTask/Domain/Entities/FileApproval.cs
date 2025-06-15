using System.ComponentModel.DataAnnotations.Schema;
using DocumentApprovalSystemTask.Domain.Enums;

namespace DocumentApprovalSystemTask.Domain.Entities
{
    public class FileApproval: BaseModel
    {
        public int ApprovalOrder { get; set; }
        public FileStatus Status { get; set; } = FileStatus.Pending;

        public DateTime? DecisionDate { get; set; }

        [ForeignKey("ResponsibleEmployee")]
        public string? ResponsibleEmployeeId { get; set; }

        [ForeignKey("File")]
        public int FileId { get; set; }
        public virtual File? File { get; set; }
        public virtual Employee? ResponsibleEmployee { get; set; }

    }
}
