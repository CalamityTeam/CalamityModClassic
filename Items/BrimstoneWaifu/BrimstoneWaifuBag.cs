using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Weapons.BrimstoneWaifu;

namespace CalamityModClassic1Point2.Items.BrimstoneWaifu
{
	public class BrimstoneWaifuBag : ModItem
	{
		public override void SetStaticDefaults()
 		{
 			//DisplayName.SetDefault("Treasure Bag");
 			//Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
 		}
		
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.expert = true;
			Item.rare = ItemRarityID.Cyan;
			//bossBagNPC = Mod.Find<ModNPC>("BrimstoneElemental").Type;
		}

		public override bool CanRightClick()
		{
			return true;
		}
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Abaddon>(), ModContent.ItemType<Brimlance>(), ModContent.ItemType<SeethingDischarge>() }));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Gehenna>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 3, 4));
        }
	}
}