using Microsoft.AspNetCore.Components.Authorization;
using Server.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Services.Authentication
{
    /// <summary>
    /// This an AuthenticationService approve all login request, do not use in production.
    /// </summary>
    public class Backdoor : AuthenticationService
    {
        private User _user;

        public override User User => _user;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = GetClaimsIdentity("Session");
            var claims = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(claims));
        }

        public override async Task<User> Login(string id, string password)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            _user = new User
            {
                Name = id,
            };

            var identity = GetClaimsIdentity("Login");
            var claims = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));

            return await Task.FromResult(_user);
        }

        public override void Logout()
        {
            _user = null;

            var identity = new ClaimsIdentity();
            var claims = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }

        private ClaimsIdentity GetClaimsIdentity(string authenticationType = null)
        {
            ClaimsIdentity identity;

            if (_user == null)
                identity = new ClaimsIdentity();
            else
                identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _user.Name)
                },authenticationType);

            return identity;
        }
    }
}