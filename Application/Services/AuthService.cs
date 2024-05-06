using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Ports;
using Shared.Common;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository user, ITokenService tokenService)
        {
            _userRepository = user;
            _tokenService = tokenService;
        }

        public async Task<TokenOut> ValidateUser(TokenIn userCredentials)
        {
            try
            {
                string? token = null;
                User CredentialsDto = MapUserToEntity(userCredentials);
                User? dataUser = await _userRepository.GetUserByKeyAsync(CredentialsDto);

                if (dataUser != null)
                {
                    if (!dataUser.IsActive)
                    {
                        return new TokenOut { Message = "El username esta inactivo.", Result = Result.IsNotActive, Token = token };
                    }

                    bool isValid = PassCheck(dataUser, userCredentials);
                    token = isValid ? _tokenService.JwtToken() : null;

                    return isValid
                        ? new TokenOut { Message = "El token se genero correctamente.", Result = Result.Success, Token = token }
                        : new TokenOut { Message = "La password es incorrecta.", Result = Result.InvalidPassword, Token = token };
                }
                else
                {
                    return new TokenOut { Message = "El username no existe.", Result = Result.NoRecords, Token = token };
                }
            }
            catch (Exception ex)
            {
                return new TokenOut { Message = $"Ha ocurrido un error. {ex.Message}", Result = Result.Error };
            }
        }

        private static bool PassCheck(User data, TokenIn user)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(user.Password);
                string encodedPassword = Convert.ToBase64String(bytes);

                return string.Equals(encodedPassword, data.Password, StringComparison.Ordinal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static User MapUserToEntity(TokenIn user)
        {
            return new User()
            {
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}