using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameForge.Data;
using GameForge.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GameForge.Controllers
{
    public class GameVersionController : Controller
    {
        private readonly GameForgeContext _context;

        public GameVersionController(GameForgeContext context)
        {
            _context = context;
        }

        // GET: GameVersion/Index/5
        // Displays the version history for a specific game
        public async Task<IActionResult> Index(int? gameId)
        {
            if (gameId == null || _context.Game == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Versions) // Include version history
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null)
            {
                return NotFound("Game not found.");
            }

            ViewBag.GameTitle = game.Title;
            return View(game.Versions);
        }

        // GET: GameVersion/Create
        // Displays a form to add a new version
        public IActionResult Create(int gameId)
        {
            ViewBag.GameId = gameId; // Pass GameId to the view
            return View();
        }

        // POST: GameVersion/Create
        // Handles the submission of a new version
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,VersionNumber,ReleaseDate,Description")] GameVersion gameVersion)
        {
            if (!ModelState.IsValid)
            {
                return View(gameVersion);
            }

            // Ensure the game exists before adding a version
            var game = await _context.Game.FindAsync(gameVersion.GameId);
            if (game == null)
            {
                return NotFound("Game not found.");
            }

            _context.GameVersion.Add(gameVersion);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Game version added successfully.";
            return RedirectToAction("Index", new { gameId = gameVersion.GameId });
        }

        // GET: GameVersion/Edit/5
        // Displays a form to edit an existing version
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GameVersion == null)
            {
                return NotFound();
            }

            var gameVersion = await _context.GameVersion.FindAsync(id);
            if (gameVersion == null)
            {
                return NotFound();
            }

            return View(gameVersion);
        }

        // POST: GameVersion/Edit/5
        // Handles the update of an existing version
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,VersionNumber,ReleaseDate,Description")] GameVersion gameVersion)
        {
            if (id != gameVersion.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(gameVersion);
            }

            try
            {
                _context.Update(gameVersion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameVersionExists(gameVersion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Success"] = "Game version updated successfully.";
            return RedirectToAction("Index", new { gameId = gameVersion.GameId });
        }

        // GET: GameVersion/Delete/5
        // Displays a confirmation to delete a version
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GameVersion == null)
            {
                return NotFound();
            }

            var gameVersion = await _context.GameVersion
                .Include(v => v.Game)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (gameVersion == null)
            {
                return NotFound();
            }

            return View(gameVersion);
        }

        // POST: GameVersion/Delete/5
        // Handles the deletion of a version
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GameVersion == null)
            {
                return Problem("Entity set 'GameVersion' is null.");
            }

            var gameVersion = await _context.GameVersion.FindAsync(id);
            if (gameVersion != null)
            {
                _context.GameVersion.Remove(gameVersion);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Game version deleted successfully.";
            return RedirectToAction("Index", new { gameId = gameVersion?.GameId });
        }

        // Helper method to check if a game version exists
        private bool GameVersionExists(int id)
        {
            return _context.GameVersion.Any(v => v.Id == id);
        }
    }
}
