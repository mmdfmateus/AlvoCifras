using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlvoCifras.Models;

namespace AlvoCifras.Controllers
{
    public class LyricsController : Controller
    {
        private readonly Context _context;

        public LyricsController(Context context)
        {
            _context = context;
        }

        // GET: Lyrics
        public async Task<IActionResult> Index()
        {
            var context = _context.Lyrics.Include(l => l.Artist);
            return View(await context.ToListAsync());
        }

        // GET: Lyrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lyrics = await _context.Lyrics
                .Include(l => l.Artist)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lyrics == null)
            {
                return NotFound();
            }

            return View(lyrics);
        }

        // GET: Lyrics/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name");
            return View();
        }

        // POST: Lyrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LyricsSong,ArtistId,Url,Id,CreatedAt")] Lyrics lyrics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lyrics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Name", lyrics.ArtistId);
            return View(lyrics);
        }

        // GET: Lyrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lyrics = await _context.Lyrics.SingleOrDefaultAsync(m => m.Id == id);
            if (lyrics == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", lyrics.ArtistId);
            return View(lyrics);
        }

        // POST: Lyrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,LyricsSong,ArtistId,Url,Id,CreatedAt")] Lyrics lyrics)
        {
            if (id != lyrics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lyrics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LyricsExists(lyrics.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", lyrics.ArtistId);
            return View(lyrics);
        }

        // GET: Lyrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lyrics = await _context.Lyrics
                .Include(l => l.Artist)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lyrics == null)
            {
                return NotFound();
            }

            return View(lyrics);
        }

        // POST: Lyrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lyrics = await _context.Lyrics.SingleOrDefaultAsync(m => m.Id == id);
            _context.Lyrics.Remove(lyrics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LyricsExists(int id)
        {
            return _context.Lyrics.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Url(string id)
        {
            //TODO: Validação

            Lyrics lyrics = await GetByUrl(id);
            ViewBag.Lyrics = lyrics.LyricsSong;


            return View(lyrics);
        }

        public async Task<Lyrics> GetByUrl(string url)
        {
            //TODO: Validação

            var lyrics = await _context.Lyrics.SingleOrDefaultAsync(m => m.Url == url);

            return lyrics;
        }

    }
}
