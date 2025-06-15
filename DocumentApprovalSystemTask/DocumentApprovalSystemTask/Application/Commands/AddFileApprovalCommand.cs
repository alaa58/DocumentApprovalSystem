using AutoMapper;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Enums;
using DocumentApprovalSystemTask.Domain.Interfaces;
using MediatR;

namespace DocumentApprovalSystemTask.Application.Commands
{
    public class AddFileApprovalCommand: IRequest<Unit>
    {
        public int ApprovalOrder { get; set; }
        public FileStatus Status { get; set; } = FileStatus.Pending;
        public DateTime? DecisionDate { get; set; }
        public string? ResponsibleEmployeeId { get; set; }
        public int FileId { get; set; }

    }
    public class AddFileApprovalCommandHandler : IRequestHandler<AddFileApprovalCommand, Unit>
    {
        private readonly IGeneralRepository<FileApproval> repository;

        public AddFileApprovalCommandHandler(IGeneralRepository<FileApproval> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(AddFileApprovalCommand request, CancellationToken cancellationToken)
        {
            var approval = new FileApproval
            {
                FileId = request.FileId,
                ResponsibleEmployeeId = request.ResponsibleEmployeeId,
                ApprovalOrder = request.ApprovalOrder,
                Status = request.Status,
                DecisionDate = request.DecisionDate
            };

            repository.Add(approval);
            await repository.SaveChangesAsync();

            return Unit.Value; 
        }
    }

}
