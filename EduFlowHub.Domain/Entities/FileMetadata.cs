using EduFlowHub.Domain.Entities.Abstractions;
using EduFlowHub.Domain.Regexes;

namespace EduFlowHub.Domain.Entities;

public class FileMetadata
    : Entity
{
    private FileMetadata()
    {
    }

    public FileMetadata(
        string path,
        string extension,
        long size)
    {
        Path = ValidatePath(path);
        Extension = ValidateExtension(extension);
        Size = ValidateSize(size);
    }

    public void GenerateGuid()
    {
        Id =  Guid.NewGuid();
    }
    
    public string Path { get; private set; } = null!;
    public string Extension { get; private set; } = null!;
    public long Size { get; private set; }
    
    private string ValidatePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new DomainValidationException("File path cannot be empty");
        }

        if (!FilePathRegex.IsMatch(path.Trim()))
        {
            throw new DomainValidationException("File path is in not valid format");
        }
        
        return path.Trim();
    }
    
    private string ValidateExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
        {
            throw new DomainValidationException("File extenstion cannot be empty");
        }

        if (!FilePathRegex.IsMatch(extension.ToLower().Trim()))
        {
            throw new DomainValidationException("File extension is in not valid format");
        }
        
        return extension.ToLower().Trim();
    }
    
    private long ValidateSize (long size)
    {
        if (size <= 0)
        {
            throw new DomainValidationException("File size cannot be negative or zero");
        }

        return size;
    }

}