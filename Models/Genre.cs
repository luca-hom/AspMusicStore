using System.ComponentModel.DataAnnotations;

namespace AspMusicStore.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        [Required]
        public string GenreName { get; set; }

        //Navigation properties
        public ICollection<Album> Albums { get; set; }
    }
}
