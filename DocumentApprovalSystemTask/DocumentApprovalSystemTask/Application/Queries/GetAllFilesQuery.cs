using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentApprovalSystemTask.Application.Queries
{
    public class GetAllFilesQuery : IRequest<List<FileDTO>> { }

    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, List<FileDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllFilesQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FileDTO>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Files
                .ProjectTo<FileDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
