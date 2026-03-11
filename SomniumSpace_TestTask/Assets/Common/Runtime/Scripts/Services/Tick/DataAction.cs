using System;

namespace Common.Services.Tick
{
    public struct DataAction
    {
        public object Data;
        public Action<object> Action;
    }
}