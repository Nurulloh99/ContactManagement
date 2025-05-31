namespace ContactSystem.Domain.Entities;

public class UserRole
{
    public long RoleId { get; set; }
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }

    public ICollection<User> Users { get; set; }
}
