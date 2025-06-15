using DocumentApprovalSystemTask.Application.DTOs;

namespace DocumentApprovalSystemTask.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDTO dto);
        Task<string?> LoginAsync(LoginDTO dto);
    }
}
