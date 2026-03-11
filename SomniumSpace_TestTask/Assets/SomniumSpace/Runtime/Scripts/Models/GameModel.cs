using SomniumSpace.Configs;
using Common.Models;

namespace SomniumSpace.Models
{
    public class GameModel:IModel
    {
        public GameModel(ConfigsSO configs)
        {
            Configs = configs;
        }

        public ConfigsSO Configs;

        public void Init() {}
        public void DeInit() {}
    }
}