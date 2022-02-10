using Microsoft.AspNetCore.Mvc;
using ACDC2022.Data;
using ACDC2022.Models.Auth;
using ACDC2022.Services;
using ACDC2022.Utilities;
using ACDC2022.Models;

namespace ACDC2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignInAsync(UserAuth userAuth)
        {
            if (userAuth == null)
                return BadRequest("Missing userAuth parameter");

            if (userAuth.Signature == null || userAuth.WalletId == null)
                return BadRequest("Missing signature or walletId");

            var user = await _userService.EnsureUserAsync(HttpContext, userAuth.WalletId);

            // Generate Http Only / Session Cookie
            HttpContext.Session.SetString(Session.Signature, userAuth.Signature);
            HttpContext.Session.SetString(Session.WalletId, userAuth.WalletId);
            HttpContext.Session.SetString(Session.UserId, user.UserId.ToString());

            return Ok(user);
        }

        [HttpPost]
        [Route("signout")]
        public async Task<IActionResult> SignOutAsync()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(Session.ApiSession);

            return Ok();
        }

        [HttpGet]
        [Route("signcheck")]
        public IActionResult SignCheck()
        {
            // Check Session Cookie
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(Session.Signature)))
            {
                var userId = HttpContext.Session.GetString(Session.UserId);
                var walletId = HttpContext.Session.GetString(Session.WalletId);
                var user = new User() { UserId = new Guid(userId), WalletId = walletId };

                return Ok(user);
            }

            return Ok(null);
        }
    }
}
