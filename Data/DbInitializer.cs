using AspMusicStore.Models;

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
            }
            context.SaveChanges();


            var albums = new Album[]
            {
                new Album{ AlbumTitle="Album Title 1", Description="Description 1", GenreID=1 },
                new Album{ AlbumTitle="Album Title 2", Description="Description 2", GenreID=2 },
                new Album{ AlbumTitle="Album Title 3", Description="Description 3", GenreID=1 },
                new Album{ AlbumTitle="Album Title 4", Description="Description 4", GenreID=2 },
            };
            foreach (Album a in albums) 
            {
                context.Albums.Add(a);
            }
            context.SaveChanges();




        }
    }

}
