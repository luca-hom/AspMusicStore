using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspMusicStore.Models
{
    public class Album
    {
        public int AlbumID { get; set; }
        [Required]
        public string AlbumTitle { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Genre")]
        public int GenreID { get; set; }
        [NotMapped]
        public string GenreName { get; set; }
        [NotMapped]
        public double? AlbumRating { get; set; }
        [NotMapped]
        public List<int>? SelectedAudioStorages { get; set; }
        [NotMapped]
        public List<int>? SelectedTracks { get; set; }

        //Navigation Properties
        public Genre Genre { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public ICollection<AudioStorage>? AudioStorages { get; set; }
    }
}
