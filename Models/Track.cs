namespace AspMusicStore.Models
{
    public class Track
    {
        public int TrackID { get; set; }
        public string TrackTitle { get; set; }
        public string TrackLyrics { get; set; }
        public int Duration { get; set; }
        public int Rating { get; set; }

        //Navigation properties
        public ICollection<TrackList> Tracks { get; set; }
        public ICollection<MusicianList> Musicians { get; set; }
    }
}
