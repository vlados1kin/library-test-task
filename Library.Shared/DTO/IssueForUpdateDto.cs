namespace Library.Shared.DTO;

public record IssueForUpdateDto
{
    public DateTime ReceiveTime { get; init; }
    public DateTime ReturnTime { get; init; }
}