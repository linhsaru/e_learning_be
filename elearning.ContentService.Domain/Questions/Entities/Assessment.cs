using elearning.ContentService.Domain.Common.Enums;
using SharedKernel.Common;
using System;
using System.Collections.Generic;

namespace elearning.ContentService.Domain.Questions.Entities
{
    /// <summary>
    /// Thực thể Đề thi / Bài kiểm tra (Assessment/Exam)
    /// </summary>
    public class Assessment : AuditableEntity<Guid>
    {
        /// <summary>
        /// Tên bài thi (VD: Placement Test 2026, HSK4 Midterm Exam)
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Loaị kỳ thi (Placement = 1, Mini = 2, Mid = 3, Final = 4, Mock = 5)
        /// </summary>
        public ExamType ExamType { get; set; }

        /// <summary>
        /// Thời gian làm bài quy định (tính theo phút)
        /// </summary>
        public int TimeLimitMinutes { get; set; }

        /// <summary>
        /// Mức điểm cần đạt để qua bài thi (tùy chọn)
        /// </summary>
        public decimal? PassScore { get; set; }

        /// <summary>
        /// Tổng số điểm tối đa của bài thi (tùy chọn)
        /// </summary>
        public decimal? TotalScore { get; set; }

        /// <summary>
        /// Số lần tối đa học viên được làm bài (tùy chọn, null nếu không giới hạn)
        /// </summary>
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// Cờ xáo trộn thứ tự các câu hỏi khi học viên bắt đầu làm bài
        /// </summary>
        public bool ShuffleQuestions { get; set; }

        /// <summary>
        /// Danh sách các tập câu hỏi được đưa vào bài thi này
        /// </summary>
        public ICollection<AssessmentQuestionSet> AssessmentQuestionSets { get; set; } = new List<AssessmentQuestionSet>();
    }
}
