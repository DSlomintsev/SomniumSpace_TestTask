using System.Collections.Generic;
using SomniumSpace.Actors.Entities.Components;

namespace SomniumSpace.Actors.Entities
{
    public interface IEntity
    {
        public List<IComponent> Components { get; }
    }
}