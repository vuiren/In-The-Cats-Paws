﻿using UnityEngine;

namespace Game_Project.Scripts.CommonLayer
{
    public interface IMyLogger
    {
        void Log(string message);

        void LogWarning(string message);
        void LogError(string message);
        int LogsCount();
    }

    public sealed class MyLogger : IMyLogger
    {
        private object _caller;

        public MyLogger(object caller)
        {
            _caller = caller;
        }

        private int _logsCount = 0;

        public void Log(string message)
        {
            var callerInfo = string.Format("[{0}]", _caller.GetType().Name);
            var frameInfo = string.Format("[Frame: {0}]", Time.frameCount);
            Debug.Log(callerInfo + " " + frameInfo + " " + message);
            _logsCount++;
        }

        public void LogWarning(string message)
        {
            var callerInfo = string.Format("[{0}]", _caller.GetType().Name);
            var frameInfo = string.Format("[Frame: {0}]", Time.frameCount);

            Debug.LogWarning(callerInfo + " " + frameInfo + " " + message);
            _logsCount++;
        }

        public void LogError(string message)
        {
            var callerInfo = string.Format("[{0}]", _caller.GetType().Name);
            var frameInfo = string.Format("[Frame: {0}]", Time.frameCount);

            Debug.LogError(callerInfo + " " + frameInfo + " " + message);
            _logsCount++;
        }

        public int LogsCount()
        {
            return _logsCount;
        }
    }
}