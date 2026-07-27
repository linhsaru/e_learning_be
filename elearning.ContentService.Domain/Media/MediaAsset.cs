using elearning.ContentService.Domain.Common.Enums;
using SharedKernel.Common;
using System;

namespace elearning.ContentService.Domain.Media
{
    /// <summary>
    /// Quản lý Tệp đa phương tiện (Hình ảnh, Video, Audio, Tài liệu, Phụ đề)
    /// </summary>
    public class MediaAsset : AuditableEntity<Guid>
    {
        /// <summary>
        /// Tên file gốc khi tải lên (VD: audio_unit1.mp3)
        /// </summary>
        public required string FileName { get; set; }

        /// <summary>
        /// Định dạng đa phương tiện (Image = 1, Video = 2, Audio = 3, Document = 4, Subtitle = 5)
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Đường dẫn tĩnh đến file (S3, CDN, MinIO...)
        /// </summary>
        public required string Url { get; set; }

        /// <summary>
        /// Dung lượng file tính bằng Bytes
        /// </summary>
        public long SizeInBytes { get; set; }

        /// <summary>
        /// Độ dài thời gian phát tính bằng giây (áp dụng cho Video/Audio, null cho Image/Doc)
        /// </summary>
        public int? Duration { get; set; }
    }
}
