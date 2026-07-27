using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Domain.Common.Enums
{
    public enum QuestionType
    {
        SingleChoice = 1,      // Chọn 1 đáp án
        MultipleChoice = 2,    // Chọn nhiều đáp án
        TrueFalse = 3,         // Đúng / Sai
        FillInBlank = 4,       // Điền vào chỗ trống
        Matching = 5,          // Nối cột
        Ordering = 6,          // Sắp xếp thứ tự
        ShortAnswer = 7,       // Trả lời ngắn
        Essay = 8,             // Tự luận
    }
}
