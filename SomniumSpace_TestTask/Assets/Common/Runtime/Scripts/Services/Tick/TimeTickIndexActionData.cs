using System;

namespace Common.Services.Tick
{
    public struct TimeTickIndexActionData
    {
        public int Index { get; set; }
        public Action<float> Action { get; set; }

        public TimeTickIndexActionData(int index, Action<float> action)
        {
            Index = index;
            Action = action;
        }
    }
    
    public struct NextFrameTickActionData
    {
        public int Frame { get; set; }
        public Action Action { get; set; }

        public NextFrameTickActionData(int frame, Action action)
        {
            Frame = frame;
            Action = action;
        }
    }
}