namespace LibraryWebApi.DTOs.Librarian
{
    public class GetLibrarianDto
    {

        public string Id { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string NationalId {  get; set; } = string.Empty;

        public string Address {  get; set; } = string.Empty;

        public DateTime StartWork { get; set; } = DateTime.MinValue;

        public string Username {  get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? RegisterManagerId { get; set; }

    }
}
