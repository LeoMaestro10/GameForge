using GameForge.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameForge.Controllers
{
    [Route("Groups")]
    public class GroupController : Controller
    {
        private readonly GameGroupingManager _groupManager;

        public GroupController()
        {
            int userId = 1; // Replace with actual logged-in user ID
            _groupManager = new GameGroupingManager(userId);
        }

        [HttpGet("Manage")]
        public IActionResult ManageGroups()
        {
            var groups = _groupManager.GetGroups();
            return View(groups);
        }

        [HttpGet("Create")]
        public IActionResult CreateGroup()
        {
            return View("Create");
        }

        public IActionResult ViewGroup(string groupName)
        {
            var group = _groupManager.GetGroups().FirstOrDefault(g => g.Name == groupName);
            if (group == null)
            {
                return NotFound("Group not found.");
            }

            return View("ViewGroup", group);
        }


        [HttpPost("Create")]
        public IActionResult CreateGroup(string groupName)
        {
            if (_groupManager.CreateGroup(groupName))
                return RedirectToAction("ManageGroups");
            ModelState.AddModelError("", "Group name already exists!");
            return View("ManageGroups", _groupManager.GetGroups());
        }

        [HttpPost("Delete")]
        public IActionResult DeleteGroup(string groupName)
        {
            _groupManager.DeleteGroup(groupName);
            return RedirectToAction("ManageGroups");
        }

        [HttpPost("AddGame")]
        public IActionResult AddGameToGroup(string groupName, int gameId,string desc,string url,string category)
        {
            // Retrieve game from DB (replace with actual logic)
            var game = new Game { Id = gameId, Title = "Sample Game",Description=desc,ImageUrl=url,Category=category };

            if (!_groupManager.AddGameToGroup(groupName, game))
            {
                ModelState.AddModelError("", "Game already in group or invalid group.");
            }

            return RedirectToAction("ManageGroups");
        }

        [HttpPost("RemoveGame")]
        public IActionResult RemoveGameFromGroup(string groupName, int gameId)
        {
            _groupManager.RemoveGameFromGroup(groupName, gameId);
            return RedirectToAction("ManageGroups");
        }
    }

}
