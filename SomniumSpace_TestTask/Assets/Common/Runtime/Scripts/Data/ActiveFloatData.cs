using System;
using Common.Utils;

namespace Common.Data
{
    public class ActiveFloatData
    {
        public event Action<float> UpdateEvent;

        private float value;

        public ActiveFloatData(float defaultValue)
        {
            value = defaultValue;
        }

        public virtual float Value
        {
            get { return value; }
            set
            {
                if (this.value != null && !this.value.Equals(value))
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
                else if(this.value == null && value != null)
                {
                    this.value = value;
                    UpdateEvent.Call(this.value);
                }
            }
        }
        
        public void CallUpdated() => UpdateEvent.Call(value);
    }
}
