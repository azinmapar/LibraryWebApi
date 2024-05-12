using LibraryWebApi.Models;

namespace LibraryWebApi.DTOs.Member
{
    public class GetMemberDto
    {

        public int MemberId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        //the day this member signed up in the library
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        //last time they renewed their membership
        public DateTime LastRenewedDate { get; set; } = DateTime.Now;        

        //librarian that registered them
        public string? RegisterLibrarianName { get; set; } = string.Empty;

    }
}
