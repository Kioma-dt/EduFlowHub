namespace EduFlowHub.Api.Contracts.Generic.Responses;

public sealed record PagedResponse<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
};