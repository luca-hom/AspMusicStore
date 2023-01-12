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
    public class TracksController : Controller
    {
        private readonly MusicStoreContext _context;

        public TracksController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
              return View(await _context.Tracks.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tracks == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Musicians)
                .FirstOrDefaultAsync(m => m.TrackID == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            ViewData["MusicianIDs"] = new MultiSelectList(_context.Musicians, "MusicianID", "MusicianName");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackID,TrackTitle,TrackLyrics,Duration,MusicianID")] Track track, List<int> selectedMusicians)
        {
            ViewData["MusicianIDs"] = new MultiSelectList(_context.Musicians, "MusicianID", "MusicianName", track.SelectedMusicians);

            AddMusiciansToTrack(track, selectedMusicians);

            if (ModelState.IsValid)
            {
                Console.WriteLine("Valid Model");
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Tracks == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(m => m.Musicians)
                .FirstOrDefaultAsync(i => i.TrackID == id);
            if (track == null)
            {
                return NotFound();
            }
            
            ViewData["MusicianIDs"] = new MultiSelectList(_context.Musicians, "MusicianID", "MusicianName", track.Musicians.Select(t => t.MusicianID).ToArray());

            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackID,TrackTitle,TrackLyrics,Duration")] Track track, List<int> selectedMusicians)
        {
            if (id != track.TrackID)
            {
                return NotFound();
            }

            await DeleteMusiciansFromTrack(track);
            AddMusiciansToTrack(track, selectedMusicians);


            if (ModelState.IsValid)
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.TrackID))
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
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tracks == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .FirstOrDefaultAsync(m => m.TrackID == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tracks == null)
            {
                return Problem("Entity set 'MusicStoreContext.Tracks'  is null.");
            }
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(int id)
        {
          return _context.Tracks.Any(e => e.TrackID == id);
        }

        private void AddMusiciansToTrack (Track track, List<int> selectedMusicians)
        {
            if (selectedMusicians != null)
            {

                track.Musicians = new List<Musician>();

                foreach (var musician in selectedMusicians)
                {
                    track.Musicians.Add(_context.Musicians.FirstOrDefault(m => m.MusicianID == musician));
                    Console.WriteLine(musician);
                }
            }
            else
            {
                Console.WriteLine("no selected Musicians");
            }
        }


        private async Task DeleteMusiciansFromTrack (Track track)
        {
            var dbTrack = this._context.Tracks.Include(m => m.Musicians)
                .SingleOrDefault(t => t.TrackID == track.TrackID);

            if (dbTrack.Musicians != null) {
                dbTrack.Musicians.Clear();
                await _context.SaveChangesAsync();
            }
 
        }
    }
}
