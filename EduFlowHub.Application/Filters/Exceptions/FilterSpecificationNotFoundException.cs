namespace EduFlowHub.Application.Filters.Exceptions;

public class FilterSpecificationNotFoundException
    : Exception
{
    public FilterSpecificationNotFoundException(string type)
        : base($"Filter specification with type {type} not found")
    {
        
    }
}