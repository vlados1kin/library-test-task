using Library.Domain.Models;

namespace Library.Shared.DTO;

public record IssueDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public BookDto Book { get; init; }
    public DateTime ReceiveTime { get; init; }
    public DateTime ReturnTime { get; init; }
}