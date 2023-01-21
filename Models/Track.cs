using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspMusicStore.Models
{
    public class Track
    {
        public int TrackID { get; set; }
        [Required]
        public string TrackTitle { get; set; }
        public string? TrackLyrics { get; set; }
        public int? Duration { get; set; }
        [NotMapped]
        public double? TrackRating { get; set; }
        [NotMapped]
        public List<int>? SelectedMusicians { get; set; }

        //Navigation properties
        public ICollection<Album> Albums { get; set; }
        public ICollection<Musician> Musicians { get; set; }
    }
}
