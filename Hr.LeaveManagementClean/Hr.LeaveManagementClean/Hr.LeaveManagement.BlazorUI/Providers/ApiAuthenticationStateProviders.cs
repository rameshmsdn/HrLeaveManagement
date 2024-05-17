﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hr.LeaveManagement.BlazorUI.Providers;

public class ApiAuthenticationStateProviders : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

    public ApiAuthenticationStateProviders(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var isTokenPresent = await _localStorageService.ContainKeyAsync("token");
        if (isTokenPresent == false)
        {
            return new AuthenticationState(user);
        }
        var saveToken = await _localStorageService.GetItemAsync<string>("token");
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(saveToken);
        if (tokenContent.ValidTo < DateTime.Now)
        {
            await _localStorageService.RemoveItemAsync("token");
            return new AuthenticationState(user);
        }

        var claims = await GetClaims();

        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        var claims = await GetClaims();
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"jwt"));
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorageService.RemoveItemAsync("token");
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }
    private async Task<List<Claim>> GetClaims()
    {
        var savedToken = await _localStorageService.GetItemAsync<string>("token");
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}
