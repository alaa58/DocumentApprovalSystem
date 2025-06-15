namespace DocumentApprovalSystemTask.Domain.Entities
{
    public class FileCategory: BaseModel
    {
        public string? Name { get; set; }
        public List<File>? files { get; set; }
    }
}
