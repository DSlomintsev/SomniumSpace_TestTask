using System.Collections.Generic;

namespace Common.Data
{
    public class ActiveModifiableFloatDataCont : IModifiableData
    {
        public float DefaultValue { get; set; }
        public float Value { get; set; }

        List<ActiveModifiableFloatData> data = new List<ActiveModifiableFloatData>();

        public void AddProperty(ActiveModifiableFloatData property)
        {
            data.Add(property);
        }

        public void AddModifier(string id, ModifierType type, float value)
        {
            for (var i = 0; i < data.Count; i++)
            {
                data[i].AddModifier(id, type, value);
            }
        }

        public void RemoveModifier(string id)
        {
            for (var i = 0; i < data.Count; i++)
            {
                data[i].RemoveModifier(id);
            }
        }

        public void SetModifier(string id, bool isAdd, ModifierType type, float value)
        {
            for (var i = 0; i < data.Count; i++)
            {
                data[i].SetModifier(id,isAdd,type,value);
            }
        }

        public void Destroy()
        {
            for (var i = 0; i < data.Count; i++)
            {
                data[i].Destroy();
            }
            data.Clear();
            data = null;
        }
    }
}