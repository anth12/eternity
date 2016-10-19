using System.Linq;

public static class StringExtensions
{
    public static string Or(this string value, string alternateValue)
    {
        return value.IsNotBlank() ? value : alternateValue;
    }

    public static bool IsBlank(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNotBlank(this string value)
    {
        return !string.IsNullOrEmpty(value);
    }

    public static string PathFileName(this string path)
    {
        var fileName = path.Split('\\').Last();
        var fileTypeIndex = fileName.LastIndexOf('.');
        return fileName.Substring(0, fileTypeIndex);
    }
}