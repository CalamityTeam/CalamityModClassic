using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point0.Items
{
	public class SlimeGodBag : ModItem
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
            //itemLoot.Add(Mod.Find<ModItem>("SlimeBlaster").Type, 2);
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("AbyssalTome").Type, 3));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("ManaOverloader").Type, 5));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("StatigelHelm").Type, 7));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("StatigelArmor").Type, 7));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("StatigelGreaves").Type, 7));
            itemLoot.Add(ItemDropRule.Common(ItemID.RoyalGel));
            itemLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 150, 199));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("PurifiedGel").Type, 1, 20, 39));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("GelDart").Type, 1, 100, 124));
        }
	}
}