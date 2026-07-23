using System.Text.RegularExpressions;

namespace SharedKernel.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string? value) =>
        string.IsNullOrWhiteSpace(value);

    public static string ToSlug(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        
        string slug = value.ToLowerInvariant().Trim();
        slug = Regex.Replace(slug, @"[áàảãạâấầẩẫậăắằẳẵặ]", "a");
        slug = Regex.Replace(slug, @"[éèẻẽẹêếềểễệ]", "e");
        slug = Regex.Replace(slug, @"[iíìỉĩị]", "i");
        slug = Regex.Replace(slug, @"[óòỏõọôốồổỗộơớờởỡợ]", "o");
        slug = Regex.Replace(slug, @"[úùủũụưứừửữự]", "u");
        slug = Regex.Replace(slug, @"[ýỳỷỹỵ]", "y");
        slug = Regex.Replace(slug, @"đ", "d");
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"\s+", " ").Trim();
        slug = Regex.Replace(slug, @"\s", "-");
        return slug;
    }
}
