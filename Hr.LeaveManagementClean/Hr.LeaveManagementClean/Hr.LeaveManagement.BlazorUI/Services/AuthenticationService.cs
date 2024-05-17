using Blazored.LocalStorage;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Providers;
using Hr.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hr.LeaveManagement.BlazorUI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider):base(client,localStorage)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authRequest = new AuthRequest()
            {
                Email = email,
                Password = password
            };
            var authenticationResponse = await _client.LoginAsync(authRequest);
            if (authenticationResponse.Token != string.Empty)
            {
                await _localStorage.SetItemAsync("token", authenticationResponse.Token);

                // set claims in Balzor and login state
                await ((ApiAuthenticationStateProviders)_authenticationStateProvider).LoggedIn();

                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task Logout()
    {
        await ((ApiAuthenticationStateProviders)_authenticationStateProvider).LoggedOut();
    }

    public async Task<bool> RegisterAsync(string firstname, string lastname, string userName, string email, string password)
    {
        RegistrationRequest registrationRequest = new RegistrationRequest()
        {
            FirstName = firstname,
            LastName = lastname,
            Email = email,
            Username = userName,
            Password = password
        };
        var response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return true;
        }
        return false;
    }
}
