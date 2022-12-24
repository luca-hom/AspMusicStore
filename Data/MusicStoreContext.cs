using AspMusicStore.Models;
using Microsoft.EntityFrameworkCore;

namespace AspMusicStore.Data
{
    public class MusicStoreContext : DbContext
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options) : base(options) 
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<AudioStorage> AudioStorages { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Musician> Musicians { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<AudioStorage>().ToTable("AudioStorage");
 
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Musician>().ToTable("Musician");

            modelBuilder.Entity<Track>().ToTable("Track");

            modelBuilder.Entity<Rating>().ToTable("Ratings");


            //Foreign Keys for Lists
/*            modelBuilder.Entity<AudioStorageList>().HasKey(a => new { a.AudioStorageID, a.AlbumID });
            modelBuilder.Entity<MusicianList>().HasKey(m => new { m.TrackID, m.MusicianID });
            modelBuilder.Entity<TrackList>().HasKey(t => new { t.AlbumID, t.TrackID });*/

        }

    }
}
