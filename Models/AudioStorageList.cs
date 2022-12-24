namespace AspMusicStore.Models
{
    public class AudioStorageList
    {
        public int AudioStorageID { get; set; }
        public int AlbumID { get; set; }


        //Navigation properties
        public AudioStorage AudioStorage { get; set; }
        public Album Album { get; set; }
    }
}
