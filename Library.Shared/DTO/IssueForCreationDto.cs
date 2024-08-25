namespace Library.Shared.DTO;

public record IssueForCreationDto
{
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    public DateTime ReceiveTime { get; init; }
    public DateTime ReturnTime { get; init; }
}