using System;

namespace elearning.ContentService.Domain.Questions.Entities
{
    /// <summary>
    /// Bảng liên kết Nhiều - Nhiều giữa Đề thi (Assessment) và Tập câu hỏi (QuestionSet)
    /// </summary>
    public class AssessmentQuestionSet
    {
        /// <summary>
        /// ID của Đề thi / Bài kiểm tra
        /// </summary>
        public Guid AssessmentId { get; set; }

        /// <summary>
        /// ID của Tập hợp câu hỏi đưa vào bài thi
        /// </summary>
        public Guid QuestionSetId { get; set; }

        /// <summary>
        /// Thứ tự xuất hiện của tập câu hỏi/phần thi này trong bài thi
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Trọng số điểm cho phần thi này trong tổng thể bài thi
        /// </summary>
        public decimal ScoreWeight { get; set; }

        /// <summary>
        /// Đề thi liên kết
        /// </summary>
        public Assessment Assessment { get; set; } = null!;

        /// <summary>
        /// Tập câu hỏi liên kết
        /// </summary>
        public QuestionSet QuestionSet { get; set; } = null!;
    }
}
