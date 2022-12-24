using AspMusicStore.Models;
using System.Security.Policy;

namespace AspMusicStore.Data
{
    public class DbInitializer
    {
        public static void Initialize(MusicStoreContext context)
        {
            context.Database.EnsureCreated();

            //Look for any Products
            if (context.Albums.Any())
            {
                return;  //DB has been seeded
            }

            var genres = new Genre[]
            {
                new Genre{GenreName="Genre 1"},
                new Genre{GenreName="Genre 2"},
                new Genre{GenreName="Genre 3"},
            };
            foreach (Genre g in genres)
            {
                context.Genres.Add(g);
                Console.WriteLine("Added genre: " + g);
            }
            context.SaveChanges();


            var albums = new Album[]
            {
                new Album{ AlbumTitle="Album Title 1", Description="Description 1", GenreID=1 },
                new Album{ AlbumTitle="Album Title 2", Description="Description 2", GenreID=2 },
                new Album{ AlbumTitle="Album Title 3", Description="Description 3", GenreID=1 },
                new Album{ AlbumTitle="Album Title 4", Description="Description 4", GenreID=2,}
            };
            foreach (Album a in albums)
            {
                context.Albums.Add(a);
                Console.WriteLine("Added album: " + a);
            }
            context.SaveChanges();

            var audioStorages = new AudioStorage[]
            {
                new AudioStorage{ AudioStorageName= "CD"},
                new AudioStorage{ AudioStorageName= "LP"},
                new AudioStorage{ AudioStorageName= "45"},
                new AudioStorage{ AudioStorageName= "Download"}
            };
            foreach (AudioStorage au in audioStorages)
            {
                context.AudioStorages.Add(au);
                Console.WriteLine("Added audioStorage: " + au);
            }
            context.SaveChanges();


            var musicians = new Musician[]
            {
                new Musician{ MusicianName="Musician Name 1"},
                new Musician{ MusicianName="Musician Name 2"},
                new Musician{ MusicianName="Musician Name 3"}
            };
            foreach (Musician m in musicians)
            {
                context.Musicians.Add(m);
                Console.WriteLine("Added musician: " + m);
            }
            context.SaveChanges();

            
            var tracks = new Track[]
            {
                new Track{ TrackTitle="Track Title 1"},
                new Track{ TrackTitle="Track Title 2"},
                new Track{ TrackTitle="Track Title 3"},
                new Track{ TrackTitle="Track Title 4"},
                new Track{ TrackTitle="Track Title 5"},
                new Track{ TrackTitle="Track Title 6"},

            };
            foreach (Track tr in tracks) 
            {
                context.Tracks.Add(tr);
                Console.WriteLine("Added tracks: " + tr);
            }
            context.SaveChanges();


            var ratings = new Rating[]
            {
                new Rating{ RatingValue= 1, TrackID= 1 },
                new Rating{ RatingValue= 2, TrackID= 1 },
                new Rating{ RatingValue= 3, TrackID= 1 },
                new Rating{ RatingValue= 2, TrackID= 2 },
                new Rating{ RatingValue= 5, TrackID= 1 },
            };
            foreach (Rating r in ratings) 
            {
                context.Ratings.Add(r);
                Console.WriteLine("Added ratings: " + r);
            }
            context.SaveChanges();

            
            ICollection<Track> trackList = new List<Track> {
                context.Tracks.FirstOrDefault(t => t.TrackID == 1),
                context.Tracks.FirstOrDefault(t => t.TrackID == 2),
                context.Tracks.FirstOrDefault(t => t.TrackID == 3)
            };

            ICollection<AudioStorage> audioStorageList = new List<AudioStorage> {
                context.AudioStorages.FirstOrDefault(a => a.AudioStorageID == 1),
                context.AudioStorages.FirstOrDefault(a => a.AudioStorageID == 2),
                context.AudioStorages.FirstOrDefault(a => a.AudioStorageID == 3)
            };

            context.Albums.Add(new Album { AlbumTitle = "Album Title 5 Full", Description = "Description 5", GenreID = 2, Tracks = trackList, AudioStorages=audioStorageList });
            context.SaveChanges();

            ICollection<Musician> musicianList = new List<Musician> {
                context.Musicians.FirstOrDefault(a => a.MusicianID == 1),
                context.Musicians.FirstOrDefault(a => a.MusicianID == 2),
                context.Musicians.FirstOrDefault(a => a.MusicianID == 3)
            };

            context.Tracks.Add(new Track { TrackTitle = "Track Title 7 Full", Musicians = musicianList } );
            context.SaveChanges();



        }
    }

}
