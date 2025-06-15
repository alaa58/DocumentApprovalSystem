using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Enums;
using DocumentApprovalSystemTask.Domain.Interfaces;
using DocumentApprovalSystemTask.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = DocumentApprovalSystemTask.Domain.Entities.File;

namespace DocumentApprovalSystemTask.Application.Commands
{
    //public class ApproveFileCommand : IRequest<Unit>
    //{
    //    public int FileApprovalId { get; set; }
    //    public FileStatus Status { get; set; }
    //}
    //public class ApproveFileCommandHandler : IRequestHandler<ApproveFileCommand, Unit>
    //{
    //    private readonly ApplicationDbContext _context;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly IMapper _mapper;

    //    public ApproveFileCommandHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    //    {
    //        _context = context;
    //        _httpContextAccessor = httpContextAccessor;
    //        _mapper = mapper;
    //    }

    //    public async Task<Unit> Handle(ApproveFileCommand request, CancellationToken cancellationToken)
    //    {
    //        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //        // بدلاً من include => نعمل projection ونرجع entity من الـ mapper
    //        var approvalDto = await _context.FileApprovals
    //            .Where(fa => fa.Id == request.FileApprovalId && fa.ResponsibleEmployeeId == userId)
    //            .ProjectTo<ApproveFileDTO>(_mapper.ConfigurationProvider)
    //            .FirstOrDefaultAsync(cancellationToken);

    //        if (approvalDto == null)
    //            throw new Exception("Approval not found or unauthorized.");

    //        // مابنعدلش في DTO، فـ لازم نجيب الـ entity
    //        var approval = await _context.FileApprovals.FindAsync(request.FileApprovalId);
    //        if (approval == null)
    //            throw new Exception("Approval not found.");

    //        // Update logic
    //        approval.Status = request.Status;
    //        approval.DecisionDate = DateTime.UtcNow;

    //        // نجيب كل الموافقات الخاصة بنفس الملف
    //        var allApprovals = await _context.FileApprovals
    //            .Where(a => a.FileId == approvalDto.FileId)
    //            .ToListAsync(cancellationToken);

    //        if (request.Status == FileStatus.Rejected)
    //        {
    //            var file = await _context.Files.FindAsync(approvalDto.FileId);
    //            if (file != null)
    //                file.Status = FileStatus.Rejected;
    //        }
    //        else
    //        {
    //            // لو دي آخر خطوة، يبقى الملف معتمد
    //            if (approvalDto.ApprovalOrder == allApprovals.Max(x => x.ApprovalOrder))
    //            {
    //                var file = await _context.Files.FindAsync(approvalDto.FileId);
    //                if (file != null)
    //                    file.Status = FileStatus.Approved;
    //            }
    //        }

    //        await _context.SaveChangesAsync();
    //        return Unit.Value;
    //    }
    //}

    public class ApproveFileCommand : IRequest<bool>
    {
        public int FileApprovalId { get; set; }
        public bool IsApproved { get; set; }
    }

    public class ApproveFileCommandHandler : IRequestHandler<ApproveFileCommand, bool>
    {
        private readonly IGeneralRepository<FileApproval> _approvalRepo;
        private readonly IGeneralRepository<File> _fileRepo;
        private readonly IMapper _mapper;

        public ApproveFileCommandHandler(
            IGeneralRepository<FileApproval> approvalRepo,
            IGeneralRepository<File> fileRepo,
            IMapper mapper)
        {
            _approvalRepo = approvalRepo;
            _fileRepo = fileRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ApproveFileCommand request, CancellationToken cancellationToken)
        {
            // Step 1: جيب الموافقة المطلوبة (Projection)
            var approvalDto = _approvalRepo.Get(a => a.Id == request.FileApprovalId)
                .ProjectTo<ApproveFileDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (approvalDto == null) return false;

            // Step 2: رجع الكيان الحقيقي (تعديل الحالة)
            var approvalEntity = _approvalRepo.Get(a => a.Id == request.FileApprovalId).FirstOrDefault();
            if (approvalEntity == null) return false;

            approvalEntity.Status = request.IsApproved ? FileStatus.Approved : FileStatus.Rejected;
            approvalEntity.DecisionDate = DateTime.UtcNow;

            _approvalRepo.Update(approvalEntity);

            // Step 3: تحقق هل كل الـ Approvals تمت موافقتها
            var otherApprovals = _approvalRepo.Get(a => a.FileId == approvalDto.FileId && a.Id != approvalDto.FileId).ToList();

            bool allApproved = request.IsApproved &&
                               otherApprovals.All(a => a.Status == FileStatus.Approved);

            // Step 4: عدّل حالة الملف لو كله Approved أو Rejected
            var file = _fileRepo.Get(f => f.Id == approvalDto.FileId).FirstOrDefault();
            if (file == null) return false;

            if (request.IsApproved && allApproved)
                file.Status = FileStatus.Approved;
            else if (!request.IsApproved)
                file.Status = FileStatus.Rejected;

            _fileRepo.Update(file);

            // Step 5: Save
            await _approvalRepo.SaveChangesAsync();
            return true;
        }
    }


}
