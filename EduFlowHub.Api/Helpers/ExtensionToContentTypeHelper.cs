namespace EduFlowHub.Api.Helpers;

public static class ExtensionToContentTypeHelper
{
    public static string GetContentTypeFromExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            throw new ArgumentException("File extension cannot be empty.", nameof(extension));

        var normalized = extension.Trim();

        if (!normalized.StartsWith('.'))
            normalized = "." + normalized;

        return normalized.ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png"            => "image/png",
            ".gif"             => "image/gif",
            ".webp"            => "image/webp",
            ".bmp"             => "image/bmp",

            ".pdf"             => "application/pdf",

            ".ppt"             => "application/vnd.ms-powerpoint",
            ".pptx"            => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".odp"             => "application/vnd.oasis.opendocument.presentation",

            ".txt"             => "text/plain",
            ".md"              => "text/markdown",

            ".mp4"             => "video/mp4",
            ".webm"            => "video/webm",
            ".mov"             => "video/quicktime",
            ".avi"             => "video/x-msvideo",
            ".mkv"             => "video/x-matroska",

            ".mp3"             => "audio/mpeg",
            ".wav"             => "audio/wav",
            ".ogg"             => "audio/ogg",
            ".m4a"             => "audio/mp4",
            ".flac"            => "audio/flac",

            ".doc"             => "application/msword",
            ".docx"            => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".odt"              => "application/vnd.oasis.opendocument.text",
            ".rtf"              => "application/rtf",
            ".xls"              => "application/vnd.ms-excel",
            ".xlsx"             => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ods"              => "application/vnd.oasis.opendocument.spreadsheet",

            _ => "application/octet-stream"
        };
    }
}