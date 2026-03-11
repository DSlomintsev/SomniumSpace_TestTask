using UnityEngine;

namespace Common.Utils
{
    public static class Layers
    {
        public const int PLAYER_NUM = 1 << 6;
        public static readonly int PLAYER_NAME = LayerMask.NameToLayer("Player");
        public const int GROUND_NUM = 1 << 7;
        public static readonly int GROUND_NAME = LayerMask.NameToLayer("Ground");
        public const int BUILDING_NUM = 1 << 8;
        public static readonly int BUILDING_NAME = LayerMask.NameToLayer("Building");
        public const int STAIRS_NUM = 1 << 9;
        public static readonly int STAIRS_NAME = LayerMask.NameToLayer("Stairs");
        public const int WALL_NUM = 1 << 10;
        public static readonly int WALL_NAME = LayerMask.NameToLayer("Wall");
        public const int INTERACT_NUM = 1 << 11;
        public static readonly int INTERACT_NAME = LayerMask.NameToLayer("Interact");
        public const int CUSTOM_ITEM_NUM = 1 << 13;
        public static readonly int CUSTOM_ITEM_NAME = LayerMask.NameToLayer("CustomItem");
        public const int UNIT_NUM = 1 << 15;
        public static readonly int UNIT_NAME = LayerMask.NameToLayer("Unit");
        
        public const int OVERLAY_NUM = 1 << 29;
        public static readonly int OVERLAY_NAME = LayerMask.NameToLayer("Overlay");
        public const int SUBCHARACTER_NUM = 1 << 30;
        public static readonly int SUBCHARACTER_NAME = LayerMask.NameToLayer("SubCharacter");
        public const int CHARACTER_NUM = 1 << 31;
        public static readonly int CHARACTER_NAME = LayerMask.NameToLayer("Character");
    }
}