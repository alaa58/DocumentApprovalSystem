using AutoMapper;
using DocumentApprovalSystemTask.Application.Commands;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;

namespace DocumentApprovalSystemTask.Application.Mappings
{
    public class AddFileProfile : Profile
    {
        public AddFileProfile()
        {
            CreateMap<AddFileDTO, DocumentApprovalSystemTask.Domain.Entities.File>().ReverseMap();
            CreateMap<AddFileDTO, AddFileCommand>();
            CreateMap<AddFileCommand, DocumentApprovalSystemTask.Domain.Entities.File>()
               .ForMember(dest => dest.Id, opt => opt.Ignore()) 
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow)); 
        }
    }
}