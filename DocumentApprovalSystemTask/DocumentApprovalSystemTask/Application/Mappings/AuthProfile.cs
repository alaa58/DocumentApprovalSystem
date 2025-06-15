using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;

namespace DocumentApprovalSystemTask.Application.Mappings
{
    public class AuthProfile: Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>();
        }
    }
}
