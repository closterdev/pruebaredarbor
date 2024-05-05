using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository user)
        {
            _configuration = configuration;
            _userRepository = user;
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
                        return new TokenOut { Message = "El username esta inactivo.", Result = Result.Error, Token = token };
                    }

                    bool isValid = PassCheck(dataUser, userCredentials);
                    token = isValid ? JwtToken() : null;

                    return isValid
                        ? new TokenOut { Message = "El token se genero correctamente.", Result = Result.Success, Token = token }
                        : new TokenOut { Message = "La password es incorrecta.", Result = Result.Error, Token = token };
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
            catch (Exception ex)
            {
                return false;
            }
        }
        private string JwtToken()
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["CredentialsJwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["CredentialsJwt:Issuer"],
                audience: _configuration["CredentialsJwt:Audience"],
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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