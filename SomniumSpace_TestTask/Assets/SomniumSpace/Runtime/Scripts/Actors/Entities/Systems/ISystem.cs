using SomniumSpace.Actors.Entities.Components;

namespace SomniumSpace.Actors.Entities.Systems
{
    public interface ISystem
    {
        public void AddComponent(IComponent component);
        void Init();
        void DeInit();
    }
}