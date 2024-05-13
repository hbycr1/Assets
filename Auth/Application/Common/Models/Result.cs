namespace Application.Common.Models;

public class Result
{
	public virtual bool IsValid { get => string.IsNullOrEmpty(Error) && Errors?.Any() != true; }
	public string? Error { get; set; } = string.Empty;
	public IEnumerable<string>? Errors { get; set; } = Enumerable.Empty<string>();
}

public class Result<T> : Result
{
	public T? Model { get; set; }
}

public class ResultListing<T> : Result
{
	public int Page { get; set; }
	public int PageSize { get; set; }
	public int TotalPages { get; set; }
	public int TotalResults { get; set; }
	public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
}
