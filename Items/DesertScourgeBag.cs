using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point0.Items
{
	public class DesertScourgeBag : ModItem
	{
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Treasure Bag");
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			//Tooltip.SetDefault("Right click to open");
			Item.rare = 9;
			Item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("VictideHelmet").Type, 7));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("VictideBreastplate").Type, 7));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("VictideLeggings").Type, 7));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("VictoryShard").Type, 1, 10, 16));
            itemLoot.Add(ItemDropRule.Common(ItemID.Coral, 1, 7, 11));
            itemLoot.Add(ItemDropRule.Common(ItemID.Seashell, 1, 7, 11));
            itemLoot.Add(ItemDropRule.Common(ItemID.Starfish, 1, 7, 11));
        }
	}
}