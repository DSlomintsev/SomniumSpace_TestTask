using System.Collections.Generic;
using SomniumSpace.Actors.Entities;
using SomniumSpace.Actors.Entities.Components;

namespace SomniumSpace.Actors.Player
{
    public class PlayerController: IEntity
    {
        private List<IComponent> _components  = new ();
        public List<IComponent> Components => _components;
    }
}