namespace Application.Interfaces
{
    public interface IAuthService
    {
        string JwtToken(string username, string password);
    }
}