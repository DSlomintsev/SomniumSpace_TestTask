using System;

namespace Common.Services.Tick
{
    public struct TimeTickActionData : IEquatable<TimeTickActionData>
    {
        public string Id { get; set; }
        public float StartTime { get; set; }
        public float EndTime { get; set; }
        public float Interval { get; set; }
        public bool IsOneTime { get; set; }
        public bool ShouldBeRemoved { get; set; }
        public Action Action { get; set; }

        public TimeTickActionData(string id, float interval, bool isOneTime, Action action)
        {
            Id = id;
            StartTime = 0;
            EndTime = 0;
            Interval = interval;
            IsOneTime = isOneTime;
            ShouldBeRemoved = false;
            Action = action;
        }

        public bool Equals(TimeTickActionData other) => StartTime.Equals(other.StartTime) && Interval.Equals(other.Interval) && IsOneTime == other.IsOneTime && ShouldBeRemoved == other.ShouldBeRemoved && Equals(Action, other.Action);

        public override bool Equals(object obj)=>obj is TimeTickActionData other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(StartTime, Interval, IsOneTime, ShouldBeRemoved, Action);
    }
}