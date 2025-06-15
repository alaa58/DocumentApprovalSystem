using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocumentApprovalSystemTask.Migrations;

namespace DocumentApprovalSystemTask.Domain.Entities
{
    public class Employee : BaseModel
    {
        [Key]
        public string UserId { get; set; }
        public string? Name { get; set; }  
        public virtual List<File>? Files {  get; set; }
        public virtual List<FileApproval>? Approvals { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

    }
}
