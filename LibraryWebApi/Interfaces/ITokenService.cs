using LibraryWebApi.Models;

namespace LibraryWebApi.Interfaces
{
    public interface ITokenService
    {

        Task<(string token, string refreshToken)> CreateToken(Librarian user);

        string? ValidateRefreshToken(string refreshToken);

    }
}
