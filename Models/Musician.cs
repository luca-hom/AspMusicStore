namespace AspMusicStore.Models
{
    public class Musician
    {
        public int MusicianID { get; set; }
        public string MusicianName { get; set; }

        //Navigation properties
         public ICollection<MusicianList> MusicianLists { get; set; }
    }
}
