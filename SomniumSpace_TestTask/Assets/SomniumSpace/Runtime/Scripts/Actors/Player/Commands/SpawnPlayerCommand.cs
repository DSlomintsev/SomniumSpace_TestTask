using SomniumSpace.Actors.Entities;
using SomniumSpace.Models;
using Common.Models;

namespace SomniumSpace.Actors.Player.Commands
{
    public static class SpawnPlayerCommand
    {
        public static void Do(bool isCurrent)
        {
            var gameModel = ModelsLocator.Get<GameModel>();
            var model = ModelsLocator.Get<PlayersModel>();

            var playerController = new PlayerController();
            
            IEntity playerEntity = playerController;
            //playerEntity.Components.Add(new PlayerSkinComponent(playerEntity,SpawnUtils.Instantiate(gameModel.Configs.Prefabs.PlayerGfx).transform));
            //playerEntity.Components.Add(new InventoryComponent(playerEntity));

            model.Players.Add(playerController);
            if (isCurrent)
                model.CurrentPlayer.Value = playerController;
        }
    }
}