namespace AspMusicStore.Models
{
    public class AudioStorage
    {
        public int AudioStorageID { get; set; }
        public string AudioStorageName { get; set; }

        //Navigation properties
        public ICollection<AudioStorageList>? AudioStorageList { get; set; }
    }
}
