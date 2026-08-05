using elearning.ContentService.Application.Questions.Commands;
using elearning.ContentService.Application.Questions.DTOs;
using elearning.ContentService.Application.Questions.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.API.Controllers.Questions
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ISender _sender;

        public QuestionsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Tạo mới một câu hỏi đơn
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateQuestionCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một câu hỏi theo ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new GetQuestionByIdQuery(id), ct);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Lấy danh sách câu hỏi thuộc một tập hợp câu hỏi (QuestionSet)
        /// </summary>
        [HttpGet("by-set/{questionSetId:guid}")]
        [ProducesResponseType(typeof(IReadOnlyList<QuestionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByQuestionSet([FromRoute] Guid questionSetId, CancellationToken ct)
        {
            var result = await _sender.Send(new GetQuestionsByQuestionSetIdQuery(questionSetId), ct);
            return Ok(result.Value);
        }

        /// <summary>
        /// Cập nhật thông tin câu hỏi
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateQuestionCommand command, CancellationToken ct)
        {
            if (id != command.Id)
            {
                return BadRequest(Error.Validation("Question.IdMismatch", "ID in route does not match ID in body."));
            }

            var result = await _sender.Send(command, ct);
            if (result.IsFailure)
            {
                return result.Error.Code.Contains("NotFound") ? NotFound(result.Error) : BadRequest(result.Error);
            }

            return NoContent();
        }

        /// <summary>
        /// Xóa một câu hỏi
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new DeleteQuestionCommand(id), ct);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }

        /// <summary>
        /// Import danh sách câu hỏi, phương án đáp án và lời giải thích từ file Excel (.xlsx, .xls)
        /// </summary>
        [HttpPost("import-excel/{questionSetId:guid}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ImportQuestionsResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportExcel([FromRoute] Guid questionSetId, IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(Error.Validation("File.Empty", "Please upload a valid Excel file."));
            }

            using var stream = file.OpenReadStream();
            var command = new ImportQuestionsFromExcelCommand(questionSetId, stream);
            var result = await _sender.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
