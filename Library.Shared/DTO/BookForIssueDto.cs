namespace Library.Shared.DTO;

public record BookForIssueDto
{
    public DateTime ReceiveTime { get; init; }
    public DateTime ReturnTime { get; init; }
}