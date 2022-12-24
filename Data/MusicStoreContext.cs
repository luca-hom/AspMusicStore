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
        public DbSet<AudioStorageList> AudioStorageLists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicianList> MusicianLists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackList> TrackLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<AudioStorage>().ToTable("AudioStorage");
            modelBuilder.Entity<AudioStorageList>().ToTable("AudioStorageList");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Musician>().ToTable("Musician");
            modelBuilder.Entity<MusicianList>().ToTable("MusicianList");
            modelBuilder.Entity<Track>().ToTable("Track");
            modelBuilder.Entity<TrackList>().ToTable("TrackList");

            //Foreign Keys for Lists
            modelBuilder.Entity<AudioStorageList>().HasKey(a => new { a.AudioStorageID, a.AlbumID });
            modelBuilder.Entity<MusicianList>().HasKey(m => new { m.TrackID, m.MusicianID });
            modelBuilder.Entity<TrackList>().HasKey(t => new { t.AlbumID, t.TrackID });

        }

    }
}
