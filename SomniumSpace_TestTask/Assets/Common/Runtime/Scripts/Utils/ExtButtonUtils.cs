using System.Collections.Generic;
using Common.Components.UI;

namespace Common.Utils
{
    public static class ExtButtonUtils
    {
        public static ExtBtn GetBtnById(this List<ExtBtn> items, string id)
        {
            ExtBtn result = null;
            for (var i = 0; i < items.Count; i++)
            {
                var uiItem = items[i];
                if (uiItem.Id == id)
                {
                    result = uiItem;
                    break;
                }
            }

            return result;
        }
    }
}