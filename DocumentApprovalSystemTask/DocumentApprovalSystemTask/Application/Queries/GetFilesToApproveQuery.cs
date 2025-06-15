using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentApprovalSystemTask.Application.Queries
{
    public class GetFilesToApproveQuery : IRequest<List<FileToApproveDTO>>
    {
        
    }
    public class GetFilesToApproveQueryHandler : IRequestHandler<GetFilesToApproveQuery, List<FileToApproveDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetFilesToApproveQueryHandler(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<FileToApproveDTO>> Handle(GetFilesToApproveQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            return await _context.FileApprovals
                .Where(fa =>
                    fa.ResponsibleEmployeeId == userId &&
                    fa.Status == Domain.Enums.FileStatus.Pending &&
                    fa.File.Approvals
                        .Where(a => a.ApprovalOrder < fa.ApprovalOrder)
                        .All(a => a.Status == Domain.Enums.FileStatus.Approved))
                .ProjectTo<FileToApproveDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}

