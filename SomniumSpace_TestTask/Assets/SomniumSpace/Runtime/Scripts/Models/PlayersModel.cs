using System.Collections.Generic;
using SomniumSpace.Actors.Player;
using Common.Data;
using Common.Models;

namespace SomniumSpace.Models
{
    public class PlayersModel:IModel
    {
        public ActiveData<PlayerController> CurrentPlayer = new ();
        public List<PlayerController> Players = new();
        
        public void Init() { }
        public void DeInit() { }
    }
}