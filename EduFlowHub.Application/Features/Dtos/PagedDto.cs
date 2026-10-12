namespace EduFlowHub.Application.Features.Dtos;

public sealed record PagedDto<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalCount)
    where T : class
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}