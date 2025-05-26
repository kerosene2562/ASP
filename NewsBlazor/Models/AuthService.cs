using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace NewsBlazor.Models
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly JwtAuthenticationStateProvider _authProvider;


        public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authProvider = (JwtAuthenticationStateProvider)authProvider;
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/accountapi/login", new { email, password });

            if (!response.IsSuccessStatusCode)
                return false;

            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", token);
            _authProvider.NotifyUserAuthentication(token);
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authProvider.NotifyUserLogout();
            _http.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> Register(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/accountapi/register", new { email, password });
            return response.IsSuccessStatusCode;
        }
    }
}
