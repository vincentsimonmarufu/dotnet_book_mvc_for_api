using BookMvcApp.Models;

namespace BookMvcApp.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7151/api/account/register", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<string> LoginAsync(LoginModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7151/api/account/login", model);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }

        return null;
    }

    public async Task<bool> LogoutAsync()
    {
        var response = await _httpClient.PostAsync("https://localhost:7151/api/account/logout", null);
        return response.IsSuccessStatusCode;
    }
}