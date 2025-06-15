using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;

namespace DocumentApprovalSystemTask.Application.Mappings
{
    public class FileApprovalProfile: Profile
    {
        public FileApprovalProfile()
        {
            _ = CreateMap<FileApproval, ApproveFileDTO>()
                .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => src.FileId))
                .ForMember(dest => dest.ApprovalOrder, opt => opt.MapFrom(src => src.ApprovalOrder))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
