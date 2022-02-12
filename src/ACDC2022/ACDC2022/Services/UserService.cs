using ACDC2022.Models;
using ACDC2022.Repositories;

namespace ACDC2022.Services;

public interface IUserService
{
    Task<User> EnsureUserAsync(HttpContext httpContext, string walletId);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> EnsureUserAsync(HttpContext httpContext, string walletId)
    {
        // Get user
        var user = _userRepository.GetUserByWalletId(walletId);

        if (user == null)
        {
            // Create user in DB
            user = await _userRepository.AddUserAsync(httpContext, walletId);
        }

        return user;
    }
}
