using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class FileMetadataRepository
    : Repository<FileMetadata>, 
        IFileMetadataRepository
        
{
    public FileMetadataRepository(DbContext dbContext)
        : base(dbContext)
    {
    }
}