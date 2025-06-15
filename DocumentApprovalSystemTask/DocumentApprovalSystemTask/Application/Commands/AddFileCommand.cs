using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Enums;
using DocumentApprovalSystemTask.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using File = DocumentApprovalSystemTask.Domain.Entities.File;

namespace DocumentApprovalSystemTask.Application.Commands
{
    public class AddFileCommand : IRequest<AddFileDTO>
    {
        [FromForm]
        public string? FileNumber { get; set; }

        [FromForm]
        public string? Subject { get; set; }

        [FromForm]
        public DateTime CreatedDate { get; set; }

        [FromForm]
        public IFormFile? Attachment { get; set; }

        [FromForm]
        public int CategoryId { get; set; }

        [FromForm]
        public List<string>? ResponsibleEmployeeIds { get; set; } 
    }

    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, AddFileDTO>
    {
        private readonly IGeneralRepository<File> repository;
        private readonly IMapper mapper;

        private readonly IHttpContextAccessor httpContextAccessor;

        public AddFileCommandHandler(IGeneralRepository<File> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddFileDTO> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException("User ID not found.");

            var file = mapper.Map<File>(request);
            file.SubmittedById = userId;

            if (request.Attachment != null && request.Attachment.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{request.Attachment.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Attachment.CopyToAsync(stream);
                }

                file.AttachmentPath = uniqueFileName;
            }

            repository.Add(file);
            await repository.SaveChangesAsync();

            var dto = mapper.Map<AddFileDTO>(file);
            dto.Id = file.Id;

            return dto;
        }

    }
}
