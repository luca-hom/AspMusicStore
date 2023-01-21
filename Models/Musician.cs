using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspMusicStore.Models
{
    public class Musician
    {
        public int MusicianID { get; set; }
        [Required]
        public string MusicianName { get; set; }
        

        //Navigation properties
         public ICollection<Track> Tracks { get; set; }
    }
}
