namespace AspMusicStore.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }

        //Navigation properties
        public ICollection<Album> Albums { get; set; }
    }
}
