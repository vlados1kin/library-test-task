namespace Library.Shared.DTO;

public record IssueForCreationDto
{
    public DateTime ReceiveTime { get; init; }
    public DateTime ReturnTime { get; init; }
}