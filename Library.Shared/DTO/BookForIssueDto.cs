namespace Library.Shared.DTO;

public record BookForIssueDto
{
    public DateTime ReceiveTime { get; set; }
    public DateTime ReturnTime { get; set; }
}