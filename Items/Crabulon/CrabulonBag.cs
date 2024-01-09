using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.BrimstoneWaifu;
using CalamityModClassic1Point2.Items.Weapons.BrimstoneWaifu;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Weapons.Crabulon;

namespace CalamityModClassic1Point2.Items.Crabulon
{
	public class CrabulonBag : ModItem
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
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true;
			//bossBagNPC = Mod.Find<ModNPC>("CrabulonIdle").Type;
		}

		public override bool CanRightClick()
		{
			return true;
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<FungalClump>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Mycoroot>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<MycelialClaws>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<HyphaeRod>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Fungicide>(), 3));
            itemLoot.Add(new CommonDrop(ItemID.GlowingMushroom, 1, 25, 35));
            itemLoot.Add(new CommonDrop(ItemID.MushroomGrassSeeds, 1, 5, 10));
        }
	}
}