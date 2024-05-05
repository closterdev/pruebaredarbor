using Application.Dtos;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenOut> ValidateUser(TokenIn userCredentials);
    }
}