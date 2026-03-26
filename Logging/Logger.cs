using System;
using UnityEngine;

namespace CSL_SimpleMetrics.Logging
{
    public class Logger
    {
        public static void Log(string message, LogLevelEnum logLevel = LogLevelEnum.Info)
        {
            Debug.Log($"[CSL-SimpleMetrics][{logLevel}] {message}");
        }

        public static void LogException(string message, Exception ex)
        {
            Debug.LogError($"[CSL-SimpleMetrics][{LogLevelEnum.Error}] {message}: {ex.Message}");
        }
    }
}
