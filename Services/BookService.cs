using BookMvcApp.Models;

namespace BookMvcApp.Services;

public class BookService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private void SetBearerToken()
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        SetBearerToken();
        var response = await _httpClient.GetAsync("https://localhost:7151/api/books");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        SetBearerToken();
        var response = await _httpClient.GetAsync("https://localhost:7151/api/authors");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Author>>();
    }
}