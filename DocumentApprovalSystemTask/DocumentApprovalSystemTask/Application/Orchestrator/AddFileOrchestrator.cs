using AutoMapper;
using DocumentApprovalSystemTask.Application.Commands;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Enums;
using MediatR;

namespace DocumentApprovalSystemTask.Application.Orchestrator
{
    public class AddFileOrchestrator : IRequest<AddFileDTO>
    {
        public AddFileCommand Command { get; }

        public AddFileOrchestrator(AddFileCommand command)
        {
            Command = command;
        }
    }

    public class AddFileOrchestratorHandler : IRequestHandler<AddFileOrchestrator, AddFileDTO>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddFileOrchestratorHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AddFileDTO> Handle(AddFileOrchestrator request, CancellationToken cancellationToken)
        {
            var command = request.Command;

            // Step 1: Add file
            var addedFile = await _mediator.Send(command);

            // Step 2: Add approvals
            int order = 1;
            if (command.ResponsibleEmployeeIds != null)
            {
                foreach (var employeeId in command.ResponsibleEmployeeIds)
                {
                    await _mediator.Send(new AddFileApprovalCommand
                    {
                        FileId = addedFile.Id,
                        ResponsibleEmployeeId = employeeId,
                        ApprovalOrder = order++,
                        Status = FileStatus.Pending
                    });
                }
            }

            return addedFile;
        }

    }
}

