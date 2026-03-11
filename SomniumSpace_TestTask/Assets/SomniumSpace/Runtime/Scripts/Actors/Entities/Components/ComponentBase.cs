namespace SomniumSpace.Actors.Entities.Components
{
    public class ComponentBase:IComponent
    {
        public IEntity Entity { get; }

        public ComponentBase(IEntity entity)
        {
            Entity = entity;
        }
    }
}