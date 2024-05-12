using LibraryWebApi.DTOs.Librarian;
using LibraryWebApi.Models;

namespace LibraryWebApi.Mappers
{
    public static class LibrarianMapper
    {

        public static GetLibrarianDto ToGetLibrarianDto(this Librarian librarian)
        {
            return new GetLibrarianDto
            {
                Id = librarian.Id,
                FullName = librarian.FullName,
                NationalId = librarian.NationalId,
                Address = librarian.Address,
                StartWork = librarian.StartWork,
                Username = librarian.UserName!,
                Email = librarian.Email!,
                PhoneNumber = librarian.PhoneNumber!,
                RegisterManagerId = librarian.RegisterManagerId!,
            };
        }

    }
}
