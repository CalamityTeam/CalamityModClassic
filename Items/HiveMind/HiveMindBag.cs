using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.HiveMind;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.HiveMind
{
	public class HiveMindBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("HiveMindP2").Type;
		}

		public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<HiveMindMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RottenBrain>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.RottenChunk, 1, 10, 20));
            itemLoot.Add(new CommonDrop(ItemID.DemoniteBar, 1, 9, 14));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TrueShadowScale>(), 1, 30, 40));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<LeechingDagger>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ShadowdropStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RotBall>(), 3, 50, 75));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PerfectDark>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Shadethrower>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DankStaff>(), 3));
        }
	}
}