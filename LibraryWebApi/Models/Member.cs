using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Models
{

    [Table("Members")] 
    public class Member
    {

        public int MemberId { get; set; } 

        public string FullName { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber {  get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        //the day this member signed up in the library
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        //last time they renewed their membership
        public DateTime LastRenewedDate {  get; set; } = DateTime.Now;

        //if they canceled their membership and when
        public DateTime? CancelDate {  get; set; }
        
        //still has a membership (by Delete this will be false
        public bool IsActive { get; set; } = true;

        //librarian that registered them
        public string RegisterLibrarianId { get; set; } = string.Empty;

        public Librarian RegisterLibrarian { get; set; } = new Librarian();

        //info on their books borrowed
        public List<Lend> Borrows { get; set; } = [];

        



    }
}
