using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMusicStore.Data;
using AspMusicStore.Models;

namespace AspMusicStore.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicStoreContext _context;

        public AlbumsController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var musicStoreContext = _context.Albums.Include(a => a.Genre);
            return View(await musicStoreContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Genre)
                .Include(a => a.AudioStorages)
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName");
            ViewData["AudioStorageIDs"] = new SelectList(_context.AudioStorages, "AudioStorageID", "AudioStorageName");
            ViewData["TrackIDs"] = new SelectList(_context.Tracks, "TrackID", "TrackTitle");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumTitle,Description,GenreID,AudioStorages,Tracks")] Album album, List<int> selectedAudioStorages, List<int> selectedTracks)
        {

            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreID", album.GenreID);
            ViewData["AudioStorageIDs"] = new SelectList(_context.AudioStorages, "AudioStorageID", "AudioStorageID", album.AudioStorages);
            ViewData["TrackIDs"] = new SelectList(_context.Tracks, "TrackID", "TrackID", album.Tracks);

            AddAudioStoragesToAlbum(album, selectedAudioStorages);
            AddTracksToAlbum(album, selectedTracks);

            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.AudioStorages)
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(i => i.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            foreach (var item in album.Tracks)
            {
                Console.WriteLine(item.TrackTitle);
            }
            foreach (var item in album.AudioStorages)
            {
                Console.WriteLine(item.AudioStorageName);
            }

            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName", album.GenreID);
            ViewData["AudioStorageIDs"] = new SelectList(_context.AudioStorages, "AudioStorageID", "AudioStorageName", album.AudioStorages.Select(a => a.AudioStorageID).ToArray());
            ViewData["TrackIDs"] = new SelectList(_context.Tracks, "TrackID", "TrackTitle", album.Tracks.Select(a => a.TrackID).ToArray());
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,AlbumTitle,Description,GenreID")] Album album, List<int> selectedAudioStorages, List<int> selectedTracks)
        {
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreID", album.GenreID);
            if (id != album.AlbumID)
            {
                return NotFound();
            }
            await DeleteAudioStoragesFromAlbum(album);
            _context.ChangeTracker.Clear();
            await DeleteTracksFromAlbum(album);
            _context.ChangeTracker.Clear();
            AddAudioStoragesToAlbum(album, selectedAudioStorages);
            AddTracksToAlbum(album, selectedTracks);
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'MusicStoreContext.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Analytics()
        {
            RatingsController.CalculateRatings(_context);
            var musicStoreContext = _context.Albums;

            return View(await musicStoreContext.ToListAsync());
        }

        private bool AlbumExists(int id)
        {
          return _context.Albums.Any(e => e.AlbumID == id);
        }

        private void AddAudioStoragesToAlbum(Album album,List<int> selectedAudioStorages)
        {
            if (selectedAudioStorages != null)
            {
                album.AudioStorages = null;
                _context.Update(album);
                album.AudioStorages = new List<AudioStorage>();
                
                foreach (var audioStorage in selectedAudioStorages)
                {
                    album.AudioStorages.Add(_context.AudioStorages.FirstOrDefault(a => a.AudioStorageID == audioStorage));
                    Console.WriteLine(audioStorage);
                }
            }
            else
            {
                Console.WriteLine("no selected AudioStorage");
            }
        }

        private void AddTracksToAlbum(Album album, List<int> selectedTracks)
        {
            if (selectedTracks != null)
            {
                album.Tracks = new List<Track>();

                foreach (var track in selectedTracks)
                {
                    album.Tracks.Add(_context.Tracks.FirstOrDefault(t => t.TrackID == track));
                    Console.WriteLine(track);
                }
            }
            else
            {
                Console.WriteLine("no selected Tracks");
            }
        }

        private async Task DeleteAudioStoragesFromAlbum(Album album)
        {
            var dbAlbum = this._context.Albums.Include(a => a.AudioStorages)
                .SingleOrDefault(a => a.AlbumID == album.AlbumID);

            if (dbAlbum.AudioStorages != null)
            {
                dbAlbum.AudioStorages.Clear();
                await _context.SaveChangesAsync();
            }



        }
        private async Task DeleteTracksFromAlbum(Album album)
        {
            var dbAlbum = this._context.Albums.Include(a => a.Tracks)
                .SingleOrDefault(a => a.AlbumID == album.AlbumID);

            if (dbAlbum.Tracks != null)
            {
                dbAlbum.Tracks.Clear();
                await _context.SaveChangesAsync();
            }
        }
    }
}
