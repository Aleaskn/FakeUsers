using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Recupera il token dal localStorage
        var token = await _localStorage.GetItemAsStringAsync("authToken");

        // Se il token è nullo o vuoto, l'utente non è autenticato
        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Validazione del token JWT
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = tokenContent.Claims;
        var claimsIdentity = new ClaimsIdentity(claims, "jwtAuthType");
        var user = new ClaimsPrincipal(claimsIdentity);

        // Restituisce lo stato dell'utente autenticato
        return new AuthenticationState(user);
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = tokenContent.Claims;
        var claimsIdentity = new ClaimsIdentity(claims, "jwtAuthType");
        var user = new ClaimsPrincipal(claimsIdentity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }
}
