using System.ComponentModel.DataAnnotations;

namespace AspMusicStore.Models
{
    public class AudioStorage
    {
        public int AudioStorageID { get; set; }
        [Required]
        public string AudioStorageName { get; set; }

        //Navigation properties
        public ICollection<Album>? Albums { get; set; }
    }
}
