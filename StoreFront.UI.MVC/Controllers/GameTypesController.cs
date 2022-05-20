using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]

    public class GameTypesController : Controller
    {
        private readonly StoreFrontContext _context;

        public GameTypesController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: GameTypes
        public async Task<IActionResult> Index()
        {
              return _context.GameTypes != null ? 
                          View(await _context.GameTypes.ToListAsync()) :
                          Problem("Entity set 'StoreFrontContext.GameTypes'  is null.");
        }

        // GET: GameTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GameTypes == null)
            {
                return NotFound();
            }

            var gameType = await _context.GameTypes
                .FirstOrDefaultAsync(m => m.GameTypeId == id);
            if (gameType == null)
            {
                return NotFound();
            }

            return View(gameType);
        }

        // GET: GameTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameTypeId,Name")] GameType gameType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameType);
        }

        // GET: GameTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GameTypes == null)
            {
                return NotFound();
            }

            var gameType = await _context.GameTypes.FindAsync(id);
            if (gameType == null)
            {
                return NotFound();
            }
            return View(gameType);
        }

        // POST: GameTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameTypeId,Name")] GameType gameType)
        {
            if (id != gameType.GameTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameTypeExists(gameType.GameTypeId))
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
            return View(gameType);
        }

        // GET: GameTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GameTypes == null)
            {
                return NotFound();
            }

            var gameType = await _context.GameTypes
                .FirstOrDefaultAsync(m => m.GameTypeId == id);
            if (gameType == null)
            {
                return NotFound();
            }

            return View(gameType);
        }

        // POST: GameTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GameTypes == null)
            {
                return Problem("Entity set 'StoreFrontContext.GameTypes'  is null.");
            }
            var gameType = await _context.GameTypes.FindAsync(id);
            if (gameType != null)
            {
                _context.GameTypes.Remove(gameType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameTypeExists(int id)
        {
          return (_context.GameTypes?.Any(e => e.GameTypeId == id)).GetValueOrDefault();
        }
    }
}
