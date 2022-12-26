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
    public class AudioStoragesController : Controller
    {
        private readonly MusicStoreContext _context;

        public AudioStoragesController(MusicStoreContext context)
        {
            _context = context;
        }

        // GET: AudioStorages
        public async Task<IActionResult> Index()
        {
              return View(await _context.AudioStorages.ToListAsync());
        }

        // GET: AudioStorages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AudioStorages == null)
            {
                return NotFound();
            }

            var audioStorage = await _context.AudioStorages
                .FirstOrDefaultAsync(m => m.AudioStorageID == id);
            if (audioStorage == null)
            {
                return NotFound();
            }

            return View(audioStorage);
        }

        // GET: AudioStorages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudioStorages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AudioStorageID,AudioStorageName")] AudioStorage audioStorage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audioStorage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audioStorage);
        }

        // GET: AudioStorages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AudioStorages == null)
            {
                return NotFound();
            }

            var audioStorage = await _context.AudioStorages.FindAsync(id);
            if (audioStorage == null)
            {
                return NotFound();
            }
            return View(audioStorage);
        }

        // POST: AudioStorages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AudioStorageID,AudioStorageName")] AudioStorage audioStorage)
        {
            if (id != audioStorage.AudioStorageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audioStorage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioStorageExists(audioStorage.AudioStorageID))
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
            return View(audioStorage);
        }

        // GET: AudioStorages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AudioStorages == null)
            {
                return NotFound();
            }

            var audioStorage = await _context.AudioStorages
                .FirstOrDefaultAsync(m => m.AudioStorageID == id);
            if (audioStorage == null)
            {
                return NotFound();
            }

            return View(audioStorage);
        }

        // POST: AudioStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AudioStorages == null)
            {
                return Problem("Entity set 'MusicStoreContext.AudioStorages'  is null.");
            }
            var audioStorage = await _context.AudioStorages.FindAsync(id);
            if (audioStorage != null)
            {
                _context.AudioStorages.Remove(audioStorage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioStorageExists(int id)
        {
          return _context.AudioStorages.Any(e => e.AudioStorageID == id);
        }
    }
}
