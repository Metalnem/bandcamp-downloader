namespace Api.Model;

public class LoginResponse
{
    public bool Ok { get; set; }
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
}
