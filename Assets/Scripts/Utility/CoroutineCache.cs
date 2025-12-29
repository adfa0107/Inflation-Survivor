using System.Collections.Generic;
using UnityEngine;

namespace adfa.Utility
{
    public static class CoroutineCache
    {
        private static readonly Dictionary<int, WaitForSeconds> _waitForSecondsCache = new Dictionary<int, WaitForSeconds>();

        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            int milliseconds = (int) seconds * 1000;

            if (milliseconds <= 0)
            {
                milliseconds = 0;
            }

            if (!_waitForSecondsCache.TryGetValue(milliseconds, out WaitForSeconds waitForSeconds))
            {
                waitForSeconds = new WaitForSeconds(milliseconds/1000f);
                _waitForSecondsCache.Add(milliseconds, waitForSeconds);
            }
            
            return waitForSeconds;
        }
    }
}