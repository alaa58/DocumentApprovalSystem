using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;
using File = DocumentApprovalSystemTask.Domain.Entities.File;

namespace DocumentApprovalSystemTask.Application.Mappings
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileDTO>()
                .ForMember(dest => dest.SubmittedByName, opt => opt.MapFrom(src => src.SubmittedBy.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
