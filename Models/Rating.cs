using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspMusicStore.Models
{
    public class Rating
    {
        public int RatingID { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }

        [ForeignKey("Track")]
        public int TrackID { get; set; }


        //Navigation properties
        public Track Track { get; set; }
    }
}
