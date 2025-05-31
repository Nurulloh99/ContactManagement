namespace ContactSystem.Domain.Entities;

public class Contact
{
    public long ContactId { get; set; }
    public string ContactName { get; set; }
    public string? ContactEmail { get; set; }
    public string ContactPhoneNumber{ get; set; }
    public string? ContactAddress{ get; set; }
    public DateTime CreatedAt { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
