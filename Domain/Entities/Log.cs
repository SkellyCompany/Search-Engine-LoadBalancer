using System.Collections.Generic;

namespace SearchEngine.LoadBalancer.Entities
{
    public class Log
    {
        public enum LogType
        {
            SUCCESS, WARNING, INFO, ERROR
        }

        public LogType Type { get; set; }
        public string Url { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}