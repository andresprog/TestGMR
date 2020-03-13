using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cofradinn.Modules.Utilities
{
    /// <summary>
    /// Esta clase es utilizada para controlar bucles que puedan colgar la aplicación
    /// </summary>
    public static class WatchDog
    {
        public static int _count { get; private set; } = 0;
        public static int _topCount { get; private set; } = 1000;
        public static bool _running { get; private set; } = false;
        public static string _usingByName { get; private set; } = string.Empty;
        public static Object _usingByObj { get; private set; } = null;

        /// <summary>
        /// init
        /// </summary>
        /// <param name="topCount">counter limit</param>
        /// <param name="usingBy">Object that is using the watchdog</param>
        public static void __Start(int topCount, string usingBy = "", Object obj = null)
        {
            if (!_running)
            {
                __Stop();
                _topCount = topCount;
                _count = 0;
                _running = true;
                _usingByName = usingBy;
                _usingByObj = obj;
                Debug.Log("Starting WatchDog");
            }
            else
            {
                Debug.LogError("WatchDog is already running, Object: " + _usingByName, _usingByObj);
            }
        }
        public static void __IncrementCount()
        {
            __IncrementCount(1);
        }
        public static void __IncrementCount(int n)
        {
            _count += n;
        }
        public static void __Stop()
        {
            _topCount = 1000;
            _count = 0;
            _usingByName = string.Empty;
            _running = false;
            _usingByObj = null;
        }
        public static void __Reset()
        {
            _count = 0;
        }
        public static bool __IsOverflow()
        {
            if (_count > _topCount)
            {
                Debug.LogError("WatchDog: Is Overflow, Name: " + _usingByName, _usingByObj);
                Debug.LogError("WatchDog: Is Overflow, Object: " + _usingByObj, _usingByObj);
                Debug.LogError("WatchDog: Is Overflow, Top Count: " + _topCount, _usingByObj);
                __Stop();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}