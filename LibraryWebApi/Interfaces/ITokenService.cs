using LibraryWebApi.Models;

namespace LibraryWebApi.Interfaces
{
    public interface ITokenService
    {

        Task<string> CreateToken(Librarian user);

        //string? ValidateRefreshToken(string refreshToken);

    }
}
