namespace EduFlowHub.Domain.Enums;

public enum MaterialType
{
    File = 0,
    Image = 1,
    Pdf = 2,
    Presentation = 3,
    Text = 4,
    Video = 5,
    Audio = 6,
    Document = 7
}

public static class MaterialTypeResolver
{
    private static readonly Dictionary<string, MaterialType> Map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"]  = MaterialType.Image,
            [".jpeg"] = MaterialType.Image,
            [".png"]  = MaterialType.Image,
            [".gif"]  = MaterialType.Image,
            [".webp"] = MaterialType.Image,
            [".bmp"]  = MaterialType.Image,
            
            [".pdf"]  = MaterialType.Pdf,
            
            [".ppt"]  = MaterialType.Presentation,
            [".pptx"] = MaterialType.Presentation,
            [".odp"]  = MaterialType.Presentation,
            
            [".txt"]  = MaterialType.Text,
            [".md"]   = MaterialType.Text,
            
            [".mp4"]  = MaterialType.Video,
            [".webm"] = MaterialType.Video,
            [".mov"]  = MaterialType.Video,
            [".avi"]  = MaterialType.Video,
            [".mkv"]  = MaterialType.Video,
            
            [".mp3"]  = MaterialType.Audio,
            [".wav"]  = MaterialType.Audio,
            [".ogg"]  = MaterialType.Audio,
            [".m4a"]  = MaterialType.Audio,
            [".flac"] = MaterialType.Audio,
            
            [".doc"]  = MaterialType.Document,
            [".docx"] = MaterialType.Document,
            [".odt"]  = MaterialType.Document,
            [".rtf"]  = MaterialType.Document,
            [".xls"]  = MaterialType.Document,
            [".xlsx"] = MaterialType.Document,
            [".ods"]  = MaterialType.Document,
        };

    public static MaterialType FromExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            throw new DomainValidationException("File extension cannot be empty");

        var normalized = extension.Trim();
        if (!normalized.StartsWith('.'))
            normalized = "." + normalized;

        return Map.TryGetValue(normalized, out var type)
            ? type
            : MaterialType.File;   // всё остальное: обычный файл (например, .zip)
    }
}