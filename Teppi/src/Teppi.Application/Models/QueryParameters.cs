using Teppi.Application.Enums;

namespace Teppi.Application.Models;

public class QueryParameters
{
    public string? SearchBy { get; set; }
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public SortType? SortType { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}