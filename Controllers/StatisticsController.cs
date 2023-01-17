using AspMusicStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMusicStore.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly MusicStoreContext _context;
        public StatisticsController(MusicStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var musicStoreContext = _context.Albums
                .Include(a => a.Genre)
                .Include(a => a.Tracks)
                .ThenInclude(t => t.Musicians);

            return View(await musicStoreContext.ToListAsync());
        }

        public async Task<IActionResult> AlbumAnalyticsAsync()
        {
            RatingsController.CalculateRatings(_context);
            var musicStoreContext = _context.Albums;

            return View(await musicStoreContext.ToListAsync());
        }

        public async Task<IActionResult> TrackAnalyticsAsync()
        {
            var musicStoreContext = _context.Ratings
                .Include(r => r.Track)
                .ThenInclude(a => a.Albums);

            RatingsController.CalculateRatings(_context);

            return View(await musicStoreContext.ToListAsync());
        }


    }
}
