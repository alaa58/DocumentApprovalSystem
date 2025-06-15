using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = DocumentApprovalSystemTask.Domain.Entities.File;

namespace DocumentApprovalSystemTask.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<File> Files { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FileApproval> FileApprovals { get; set; }
        public DbSet<FileCategory> FileCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
