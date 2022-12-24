namespace AspMusicStore.Models
{
    public class MusicianList
    {
        public int TrackID { get; set; }
        public int MusicianID { get; set; }

        //Navigation properties
        public Track Track { get; set; }
        public Musician Musician { get; set; }
    }
}
