using SomniumSpace.Configs.UI;
using Common.Services;
using UnityEngine;

namespace SomniumSpace.Services.Resource
{
    public class ResourceService : IService
    {
        public const string DEFAULT = "Default";
        private IconsSO _iconsSo;

        public ResourceService(IconsSO iconsSo)
        {
            _iconsSo = iconsSo;
        }
        public void Init()
        {
        }

        public void DeInit()
        {
        }

        public Sprite GetIcon(string id)
        {
            var sprite = _iconsSo.Items.GetSprite(id);
            if (sprite == null)
            {
                Debug.LogWarning($"No icon found with id {id}");
                sprite = _iconsSo.Items.GetSprite(DEFAULT);
            }
            return sprite;
        }
    }
}