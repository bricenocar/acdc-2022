namespace ACDC2022.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string WalletId { get; set; }
    public string IpAddress { get; set; }
}
