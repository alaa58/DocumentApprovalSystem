using DocumentApprovalSystemTask.Application.Commands;
using DocumentApprovalSystemTask.Application.DTOs;
using DocumentApprovalSystemTask.Application.Orchestrator;
using DocumentApprovalSystemTask.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApprovalSystemTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddFileWithApprovals([FromForm] AddFileCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new AddFileOrchestrator(command));

            return Ok(result);
        }
        [Authorize]
        [HttpGet("files-to-approve")]
        public async Task<IActionResult> GetFilesToApprove()
        {
            var result = await _mediator.Send(new GetFilesToApproveQuery());
            return Ok(result);
        }
        [Authorize]
        [HttpPost("approve")]
        public async Task<IActionResult> ApproveFile([FromBody] ApproveFileCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("FileApproval not found or update failed");

            return Ok(new { message = "Approval status updated successfully." });
        }
        [HttpGet]
        public async Task<ActionResult<List<FileDTO>>> GetAllFiles()
        {
            var result = await _mediator.Send(new GetAllFilesQuery());
            return Ok(result);
        }
    }
}
