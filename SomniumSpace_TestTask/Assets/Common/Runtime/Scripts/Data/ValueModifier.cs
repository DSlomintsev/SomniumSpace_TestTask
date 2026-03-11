namespace Common.Data
{
    public class ValueModifier
    {
        public string Id;
        public ModifierType Type;
        public float Value;

        public ValueModifier(string id, ModifierType type, float value)
        {
            Id = id;
            Type = type;
            Value = value;
        }
    }
}