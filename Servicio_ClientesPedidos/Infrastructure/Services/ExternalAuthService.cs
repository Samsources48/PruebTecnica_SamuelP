using Application.DTOs;
using Application.Interfaces;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.Services;

public class ExternalAuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _authServiceUrl;
    private string _defaultAuthUrl = "https://localhost:7119";

    public ExternalAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _authServiceUrl = configuration["ExternalServices:AuthApi"] ?? _defaultAuthUrl;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/api/Auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
         
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<LoginResponseDto>(content, options);
                
                if (result != null)
                {
                    result.Success = true;
                    return result;
                }
            }

            return new LoginResponseDto 
            { 
                Success = false, 
                Message = $"Auth failed with status code {response.StatusCode}" 
            };
        }
        catch (Exception ex)
        {
            return new LoginResponseDto 
            { 
                Success = false, 
                Message = $"Connection error: {ex.Message}" 
            };
        }
    }

    public async Task<RegisterUserResponseDto> RegisterAsync(RegisterUserRequestDto request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/api/Usuarios", request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<RegisterUserResponseDto>(content, options);

                if (result != null)
                {
                    result.Success = true;
                    return result;
                }
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            return new RegisterUserResponseDto
            {
                Success = false,
                Message = $"Registration failed with status code {response.StatusCode}: {errorContent}"
            };
        }
        catch (Exception ex)
        {
            return new RegisterUserResponseDto
            {
                Success = false,
                Message = $"Connection error: {ex.Message}"
            };
        }
    }
}
