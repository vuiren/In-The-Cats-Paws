using UnityEngine;

namespace Game_Code
{
    public interface ILogger
    {
        void Log(string message);

        void LogWarning(string message);
        void LogError(string message);
        int LogsCount();
    }
    
    public class Logger: ILogger
    {
        private int _logsCount = 0;
        
        public void Log(string message)
        {
            Debug.Log(message);
            _logsCount++;
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
            _logsCount++;
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
            _logsCount++;
        }

        public int LogsCount()
        {
            return _logsCount;
        }
    }
}