using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMusicStore.Data;
using AspMusicStore.Models;
using Microsoft.IdentityModel.Logging;

namespace AspMusicStore.Controllers
{
    public class RatingsController : Controller
    {
        private readonly MusicStoreContext _context;

        public RatingsController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var musicStoreContext = _context.Ratings.Include(r => r.Track);
            return View(await musicStoreContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ratings == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(r => r.Track)
                .FirstOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["TrackID"] = new SelectList(_context.Tracks, "TrackID", "TrackTitle");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingValue,TrackID")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrackID"] = new SelectList(_context.Tracks, "TrackID", "TrackTitle", rating.TrackID);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ratings == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["TrackID"] = new SelectList(_context.Tracks, "TrackID", "TrackTitle", rating.TrackID);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingValue,TrackID")] Rating rating)
        {
            if (id != rating.RatingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingID))
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
            ViewData["TrackID"] = new SelectList(_context.Tracks, "TrackID", "TrackTitle", rating.TrackID);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ratings == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(r => r.Track)
                .FirstOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ratings == null)
            {
                return Problem("Entity set 'MusicStoreContext.Ratings'  is null.");
            }
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public static void CalculateRatings(MusicStoreContext externalContext)
        {
                var musicStoreContext = externalContext.Ratings
                    .Include(r => r.Track)
                    .ThenInclude(a => a.Albums);

                var albumContext = externalContext.Albums
                    .Include(t => t.Tracks)
                    .ToList();


                foreach (var item in musicStoreContext)
                {
                    var trackRatingList = externalContext.Ratings
                        .Where(r => r.TrackID == item.TrackID)
                        .Select(s => s.RatingValue)
                        .ToList();

                    item.Track.TrackRating = trackRatingList.Average();
                }


                foreach (var album in albumContext)
                {
                    Console.WriteLine(album.AlbumTitle);

                    if (album.Tracks != null)
                    {
                        List<double?> trackArray = new List<double?> { };
                        foreach (var track in album.Tracks)
                        {
                            trackArray.Add(track.TrackRating);
                            Console.WriteLine(track.TrackRating);
                        }
                        Console.WriteLine(trackArray.Average());
                        album.AlbumRating = trackArray.Average();

                    }

                }
        }

        private bool RatingExists(int id)
        {
          return _context.Ratings.Any(e => e.RatingID == id);
        }
    }
}
