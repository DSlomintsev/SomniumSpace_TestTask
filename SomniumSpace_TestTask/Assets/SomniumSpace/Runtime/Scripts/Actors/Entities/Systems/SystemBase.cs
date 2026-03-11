using SomniumSpace.Actors.Entities.Components;

namespace SomniumSpace.Actors.Entities.Systems
{
    public class SystemBase:ISystem
    {
        public virtual void AddComponent(IComponent component)
        {
        }

        public virtual void Init()
        {
        }

        public virtual void DeInit()
        {
        }
    }
}