using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;

namespace DocumentApprovalSystemTask.Application.Mappings
{
    public class FileToApproveProfile : Profile
    {
        public FileToApproveProfile()
        {
              CreateMap<FileApproval, FileToApproveDTO>()
                  .ForMember(dest => dest.FileNumber, opt => opt.MapFrom(src => src.File.FileNumber))
                  .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.File.Subject))
                  .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.File.CreatedDate));
        }
    }
}
