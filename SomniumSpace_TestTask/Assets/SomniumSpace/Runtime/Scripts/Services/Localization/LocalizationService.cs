using Common.Services.Localization;

namespace SomniumSpace.Services.Localization
{
    public class LocalizationService : LocalizationServiceBase<string>
    {
        public const string ITEM_TITLE_POSTIFX = "_title";
        public const string ITEM_DESCRIPTION_POSTIFX = "_description";

        protected override void AddStrings()
        {
            // receive string from external service, or even use i2localization
            // Items
            AddString("GOLD_GENERATOR_0_title", "Small Generator");
            AddString("GOLD_GENERATOR_0_description", "Generates 1 gold per sec");
            AddString("GOLD_GENERATOR_1_title", "Medium Generator");
            AddString("GOLD_GENERATOR_1_description", "Generates 10 gold per sec");
            AddString("GOLD_GENERATOR_2_title", "Maxi Generator");
            AddString("GOLD_GENERATOR_2_description", "Generates 100 gold per sec");
            
            AddString("SHIELD_0_title", "Small shield");
            AddString("SHIELD_0_description", "Small non magic shield");
            AddString("SHIELD_1_title", "Small shield");
            AddString("SHIELD_1_description", "Small non magic shield");
            AddString("SHIELD_2_title", "Small shield");
            AddString("SHIELD_2_description", "Small non magic shield");
            
            AddString("SKIN_0_title", "Green skin");
            AddString("SKIN_0_description", "Some description");
            
            // Inventory Categories
            AddString("NONE", "ALL");
            AddString("ATTACK", "ATTACK");
            AddString("DEFENSE", "DEFENSE");
            AddString("SKIN", "SKIN");
            
            // Shop Categories
            AddString("QQQQQ", "QQQQQ");
            AddString("LightWeapon", "Light Weapon");
            AddString("MoreDangerousWeapon", "More Dangerous Weapon");
            AddString("LightDef", "Light Defence");
            AddString("BetterDef", "Better Defence");
            AddString("Events", "Events");
            
            // Shop pack titles
            AddString("GOLD_GENERATOR_0_ShopW_title", "Small Generator");
            AddString("GOLD_GENERATOR_0_ShopW_description", "Generates 1 gold per sec");
            
            AddString("GOLD_GENERATOR_0_Shop_title", "Small Generator");
            AddString("GOLD_GENERATOR_0_Shop_description", "Generates 1 gold per sec");
            AddString("GOLD_GENERATOR_1_Shop_title", "Medium Generator");
            AddString("GOLD_GENERATOR_1_Shop_description", "Generates 10 gold per sec");
            AddString("GOLD_GENERATOR_2_Shop_title", "Maxi Generator");
            AddString("GOLD_GENERATOR_2_Shop_description", "Generates 10 gold per sec");
            AddString("Shield_0_Shop_title", "Shield 0");
            AddString("Shield_0_Shop_description", "Shield 0 descr");
            AddString("Shield_1_Shop_title", "Shield 1");
            AddString("Shield_1_Shop_description", "Shield 1 descr");
            AddString("Shield_2_Shop_title", "Shield 2");
            AddString("Shield_2_Shop_description", "Shield 2 descr");
            AddString("AllInOnePack_title", "All In One Pack");
            AddString("AllInOnePack_description", "Cool one");
            AddString("Skin_title", "Skin pack");
            AddString("Skin_description", "Green");

            // Dialog titles
            AddString("Inventory", "Inventory");
            AddString("Shop", "Shop");
        }
    }
}