using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Enums;

namespace DocumentApprovalSystemTask.Application.Mappings
{
    public class AddProfileApprovalProfile : Profile
    {
        public AddProfileApprovalProfile()
        {
            //CreateMap<AddFileApprovalDTO, FileApproval>().ReverseMap();
            CreateMap<string, FileApproval>()
           .ForMember(dest => dest.ResponsibleEmployeeId, opt => opt.MapFrom(src => src))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => FileStatus.Pending))
           .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
