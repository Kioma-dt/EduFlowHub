using System.Collections.Immutable;
using EduFlowHub.Application.Abstractions.Services.File;
using MimeDetective;
using MimeDetective.Engine;

namespace EduFlowHub.Infrastructure.FileServices.FileCheckers;

public class MimeDetectiveFileContentTypeChecker
    : IFileContentTypeChecker
{
    private const int HeaderSize = 64 * 1024;
    
    private readonly IContentInspector _inspector = new ContentInspectorBuilder()
    {
        Definitions = MimeDetective.Definitions.DefaultDefinitions.All()
    }.Build();
    
    public bool IsContentTypeCorrect(
        Stream fileStream, 
        string fileExtension)
    {
        var buffer = new byte[HeaderSize];
        int read = fileStream.Read(buffer, 0, buffer.Length);
        fileStream.Position = 0;
        
        ImmutableArray<DefinitionMatch> results = _inspector.Inspect(buffer.AsSpan(0, read).ToArray());

        FileExtensionMatch? byExtension = results.ByFileExtension().FirstOrDefault();

        if (byExtension is null)
        {
            return false;
        }
        
        return fileExtension.Equals("." + byExtension.Extension, StringComparison.OrdinalIgnoreCase);
    }
}