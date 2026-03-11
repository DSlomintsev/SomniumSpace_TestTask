using System;
using System.Collections.Generic;

namespace Common.Services.Tick
{
    public static class TickServiceUtils
    {
        public static TimeTickActionData GetById(this List<TimeTickActionData> items, string id)
        {
            TimeTickActionData result = default;
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.Id == id)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }
        
        public static int GetIndexByAction(this List<TimeTickIndexActionData> items, Action<float> action)
        {
            for (var i = 0; i < items.Count; i++)
                if (action == items[i].Action)
                    return i;

            return -1;
        }
    }
}