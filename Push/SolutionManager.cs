using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace Push
{
    public class SolutionManager
    {
        const string username = "ernie";
        const string password = "";
        private int LevelNumber = 0;

        private readonly WebClient _WebClient;

        public SolutionManager()
        {
            _WebClient = new WebClient();
        }

        public int GetCurrentLevel()
        {
            return 0;
        }

        public void GoToLevel(int level)
        {
            var url = $"http://hacker.org/push/?name={username}&password={password}&gotolevel={level}&go=Go+To+Level";
            _WebClient.DownloadString(url);
            LevelNumber = level;
        }

        public string GetCurrent()
        {
            var url = $"http://hacker.org/push/?name={username}&password={password}";
            return _WebClient.DownloadString(url);
        }

        public void Start()
        {
            if (LevelNumber == 0)
            {
                throw new Exception("Cannot start since I dont know what level I'm on.");
            }
            var data = GetCurrent();
            var b = new Board(data, LevelNumber);
        }
    }
}