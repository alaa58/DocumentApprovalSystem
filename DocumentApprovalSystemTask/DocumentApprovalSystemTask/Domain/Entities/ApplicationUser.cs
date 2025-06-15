using Microsoft.AspNetCore.Identity;

namespace DocumentApprovalSystemTask.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        List<Employee>? employees {  get; set; }
    }
}
