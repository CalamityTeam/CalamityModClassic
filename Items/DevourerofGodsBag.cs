using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point0.Items
{
	public class DevourerofGodsBag : ModItem
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
            itemLoot.Add(ItemDropRule.Common(ItemID.NebulaHelmet, 6));
            itemLoot.Add(ItemDropRule.Common(ItemID.NebulaBreastplate, 6));
            itemLoot.Add(ItemDropRule.Common(ItemID.NebulaLeggings, 6));
            itemLoot.Add(ItemDropRule.Common(ItemID.FireworksLauncher, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.LunarFlareBook, 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.NebulaBlaze, 2));
            itemLoot.Add(ItemDropRule.Common(ItemID.NebulaArcanum, 2));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("BladeofEnmity").Type, 14));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("NuclearFury").Type, 14));
            itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("NebulousCore").Type));
            itemLoot.Add(ItemDropRule.Common(ItemID.LunarOre, 1, 100, 149));
            itemLoot.Add(ItemDropRule.Common(ItemID.FragmentNebula, 1, 100, 124));
        }
	}
}