using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Providers;

namespace Hr.LeaveManagement.BlazorUI.Pages;

public partial class Home
{
    [Inject]
    private AuthenticationStateProvider authenticationStateProvider { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }


    protected async override Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProviders) authenticationStateProvider)
            .GetAuthenticationStateAsync();
    }

    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }
    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }
    protected async void Logout()
    {
        await AuthenticationService.Logout();
    }
}