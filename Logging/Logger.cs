using System;
using UnityEngine;

namespace CSL_SimpleMetrics.Logging
{
    public class Logger
    {
        public static void Log(string message, LogLevelEnum logLevel = LogLevelEnum.Info)
        {
            Debug.Log($"[{logLevel}] {message}");
        }

        public static void LogException(string message, Exception ex)
        {
            Debug.LogError($"[{LogLevelEnum.Error}] {message}: {ex.Message}");
        }
    }
}
