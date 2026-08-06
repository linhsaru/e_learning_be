using elearning.ContentService.Application.MasterData.Commands;
using elearning.ContentService.Application.MasterData.DTOs;
using elearning.ContentService.Application.MasterData.Queries;
using elearning.ContentService.Domain.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;
using SharedKernel.Pagination;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace elearning.ContentService.API.Controllers.MasterData
{
    [ApiController]
    [Route("api/v1/master-data")]
    public class MasterDataController : ControllerBase
    {
        private readonly ISender _sender;

        public MasterDataController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Tạo mới một mục Master Data theo loại (languages, levels, tags, parts-of-speech)
        /// </summary>
        [HttpPost("{type}")]
        [ProducesResponseType(typeof(MasterDataItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateMasterDataItem(
            [FromRoute] string type,
            [FromBody] CreateMasterDataItemPayload payload,
            CancellationToken ct)
        {
            var command = new CreateMasterDataItemCommand(type, payload);
            var result = await _sender.Send(command, ct);

            if (result.IsFailure)
            {
                return result.Error.Code.Contains("Exists")
                    ? Conflict(result.Error)
                    : BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetMasterDataItem), new { type = type, id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Lấy chi tiết một mục Master Data theo ID
        /// </summary>
        [HttpGet("{type}/{id}")]
        [ProducesResponseType(typeof(MasterDataItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMasterDataItem(
            [FromRoute] string type,
            [FromRoute] string id,
            CancellationToken ct)
        {
            var query = new GetMasterDataItemQuery(type, id);
            var result = await _sender.Send(query, ct);

            if (result.IsFailure)
            {
                return result.Error.Code.Contains("NotFound")
                    ? NotFound(result.Error)
                    : BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Lấy danh sách các mục Master Data (hỗ trợ lọc theo từ khóa, ngôn ngữ, loại thẻ và phân trang)
        /// </summary>
        [HttpGet("{type}")]
        [ProducesResponseType(typeof(PagedResult<MasterDataItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListMasterDataItems(
            [FromRoute] string type,
            [FromQuery] string? search = null,
            [FromQuery] Guid? languageId = null,
            [FromQuery] TagType? tagType = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var query = new ListMasterDataItemsQuery(type, search, languageId, tagType, page, pageSize);
            var result = await _sender.Send(query, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Cập nhật thông tin một mục Master Data theo ID
        /// </summary>
        [HttpPut("{type}/{id}")]
        [ProducesResponseType(typeof(MasterDataItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateMasterDataItem(
            [FromRoute] string type,
            [FromRoute] string id,
            [FromBody] UpdateMasterDataItemPayload payload,
            CancellationToken ct)
        {
            var command = new UpdateMasterDataItemCommand(type, id, payload);
            var result = await _sender.Send(command, ct);

            if (result.IsFailure)
            {
                if (result.Error.Code.Contains("NotFound"))
                    return NotFound(result.Error);
                if (result.Error.Code.Contains("Exists"))
                    return Conflict(result.Error);
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Xóa một mục Master Data (Soft delete, có kiểm tra ràng buộc trước khi xóa)
        /// </summary>
        [HttpDelete("{type}/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMasterDataItem(
            [FromRoute] string type,
            [FromRoute] string id,
            CancellationToken ct)
        {
            var command = new DeleteMasterDataItemCommand(type, id);
            var result = await _sender.Send(command, ct);

            if (result.IsFailure)
            {
                return result.Error.Code.Contains("NotFound")
                    ? NotFound(result.Error)
                    : BadRequest(result.Error);
            }

            return NoContent();
        }

        /// <summary>
        /// Sắp xếp lại thứ tự hiển thị danh sách mục Master Data
        /// </summary>
        [HttpPost("{type}/reorder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReorderMasterDataItems(
            [FromRoute] string type,
            [FromBody] List<MasterDataReorderItemDto> orderList,
            CancellationToken ct)
        {
            var command = new ReorderMasterDataItemsCommand(type, orderList);
            var result = await _sender.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
