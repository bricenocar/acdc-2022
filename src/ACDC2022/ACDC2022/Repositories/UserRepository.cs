using ACDC2022.Data;
using ACDC2022.Models;
using ACDC2022.Utilities;

namespace ACDC2022.Repositories
{
    public interface IUserRepository
    {
        User GetUserByWalletId(string walletId);
        Task<User> AddUserAsync(HttpContext httpContext, string walletId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ACDC2022DbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ACDC2022DbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public User? GetUserByWalletId(string walletId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.WalletId.Equals(walletId, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<User> AddUserAsync(HttpContext httpContext, string walletId)
        {
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                WalletId = walletId, 
                IpAddress = HttpClientUtilities.GetClientIPAddress(httpContext)
            };

            await _dbContext.Users.AddAsync(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }
    }
}
