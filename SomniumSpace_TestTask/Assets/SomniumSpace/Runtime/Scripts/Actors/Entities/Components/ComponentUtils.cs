namespace SomniumSpace.Actors.Entities.Components
{
    public static class ComponentUtils
    {
        public static T GetComponent<T>(this IEntity entity) where T : IComponent
        {
            var components = entity.Components;
            foreach (var component in components)
            {
                if (component is T comp)
                    return comp;
            }
            return default;
        }
    }
}