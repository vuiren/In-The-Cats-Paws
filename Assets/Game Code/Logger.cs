using UnityEngine;

namespace Game_Code
{
    public interface ILogger
    {
        void Log(object caller, string message);

        void LogWarning(string message);
        void LogError(string message);
        int LogsCount();
    }
    
    public class Logger: ILogger
    {
        private int _logsCount = 0;
        
        public void Log(object caller, string message)
        {
            var callerInfo = string.Format("[{0}]", caller.GetType().Name);
            Debug.Log(callerInfo+" " + message);
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