using Microsoft.AspNetCore.Components.Authorization;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Services.Authentication
{
    /// <summary>
    /// The base class of authentication.
    /// </summary>
    public abstract class AuthenticationService : AuthenticationStateProvider
    {
        public delegate Task NotifyStateChangedAsyncHandler();

        /// <summary>
        ///     Gets the <see cref="Data.User" /> object.
        /// </summary>
        public abstract User User { get; }

        /// <summary>
        ///     Login with user id and password.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="password">User password.</param>
        /// <returns>An <see cref="Data.User" /> object, <see langword="null" /> when login not success.</returns>
        public abstract Task<User> Login(string id, string password);

        /// <summary>
        ///     Logout the system.
        /// </summary>
        public abstract void Logout();

        public event NotifyStateChangedAsyncHandler LoginEventAsync;
        public event NotifyStateChangedAsyncHandler LogoutEventAsync;

        protected async Task NotifyLoginAsync(Task<AuthenticationState> task)
        {
            if (LoginEventAsync != null)
            {
                var delegates = LoginEventAsync.GetInvocationList();
                foreach (var @delegate in delegates)
                {
                    if (@delegate is null) continue;
                    var func = (NotifyStateChangedAsyncHandler)@delegate;
                    await func?.Invoke();
                }
            }

            NotifyAuthenticationStateChanged(task);
        }

        protected async Task NotifyLogoutAsync(Task<AuthenticationState> task)
        {
            if (LogoutEventAsync != null)
            {
                var delegates = LogoutEventAsync.GetInvocationList();
                foreach (var @delegate in delegates)
                {
                    if (@delegate is null) continue;
                    var func = (NotifyStateChangedAsyncHandler)@delegate;
                    await func?.Invoke();
                }
            }

            NotifyAuthenticationStateChanged(task);
        }
    }
}
