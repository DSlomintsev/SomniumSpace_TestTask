namespace Common.Data
{
    public interface IModifiableData
    {
        void SetModifier(string id, bool isAdd, ModifierType type, float value);
        void AddModifier(string id, ModifierType type, float value);
        void RemoveModifier(string id);
        float DefaultValue { get; set; }
        float Value { get; set; }
        void Destroy();
    }
}