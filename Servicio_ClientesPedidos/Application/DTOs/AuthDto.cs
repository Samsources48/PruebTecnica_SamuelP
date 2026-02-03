namespace Application.DTOs;

public class LoginRequestDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class RegisterUserRequestDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<long> RoleIds { get; set; } = new();
}
public class RegisterUserResponseDto
{
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public List<string> Roles { get; set; } = new();
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

