namespace AspMusicStore.Models
{
    public class TrackList
    {
        public int AlbumID { get; set; }
        public int TrackID { get; set; }

        //Navigation properties
        public Album Album { get; set; }
        public Track Track { get; set; }
    }
}
