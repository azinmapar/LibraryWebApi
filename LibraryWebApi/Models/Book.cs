using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Models
{

    [Table("Books")]
    public class Book
    {

        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public DateTime Released { get; set; } = DateTime.MinValue;

        // instead of deleting this will be false
        public bool Available = true;

        public List<BookEdition> Editions { get; set; } = [];


    }
}
