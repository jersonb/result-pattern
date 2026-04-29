using MyResult;

namespace StoreExample.Application.Models;

public static class ApplicationErrors
{
    public const string CANCELED = "CANCELED";
    public const string NOT_FOUND = "NOT_FOUND";
    public const string UNEXPECTED = "UNEXPECTED";
    public const string EF_LINQ_QUERY = "EF_LINQ_QUERY";

    public static ErrorResult Canceled { get; } = CANCELED;
    public static ErrorResult NotFound { get; } = NOT_FOUND;
    public static ErrorResult Unexpected { get; } = UNEXPECTED;
    public static ErrorResult EfLinkQuery { get; } = EF_LINQ_QUERY;
}