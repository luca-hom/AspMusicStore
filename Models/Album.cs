namespace AspMusicStore.Models
{
    public class Album
    {
        public int AlbumID { get; set; } 
        public string AlbumTitle { get; set; }
        public string Description { get; set; }

        public int GenreID { get; set; }

        //Navigation Properties
        public ICollection<Genre> Genres { get; set; }
        public ICollection<TrackList> Tracks { get; set; }
        public ICollection<AudioStorageList>? AudioStorageLists { get; set; }
    }
}
