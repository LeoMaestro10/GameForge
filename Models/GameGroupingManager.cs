using System.Text.RegularExpressions;

namespace GameForge.Models
{
    public class GameGroupingManager
    {
        private readonly List<Group> _groups = new();
        private readonly int _userId;

        public GameGroupingManager(int userId)
        {
            _userId = userId;
        }

        // Create a new group
        public bool CreateGroup(string groupName)
        {
            if (_groups.Any(g => g.Name == groupName)) return false; // No duplicate group names
            _groups.Add(new Group { Name = groupName, UserId = _userId });
            return true;
        }

        // Delete a group
        public bool DeleteGroup(string groupName)
        {
            var group = _groups.FirstOrDefault(g => g.Name == groupName && g.UserId == _userId);
            if (group == null) return false;
            _groups.Remove(group);
            return true;
        }

        // Add a game to a group
        public bool AddGameToGroup(string groupName, Game game)
        {
            var group = _groups.FirstOrDefault(g => g.Name == groupName && g.UserId == _userId);
            if (group == null || group.Games.Contains(game)) return false;
            group.Games.Add(game);
            return true;
        }

        // Remove a game from a group
        public bool RemoveGameFromGroup(string groupName, int gameId)
        {
            var group = _groups.FirstOrDefault(g => g.Name == groupName && g.UserId == _userId);
            if (group == null) return false;

            var game = group.Games.FirstOrDefault(g => g.Id == gameId);
            if (game == null) return false;

            group.Games.Remove(game);
            return true;
        }

        // Get all groups
        public List<Group> GetGroups() => _groups.Where(g => g.UserId == _userId).ToList();
    }

}
