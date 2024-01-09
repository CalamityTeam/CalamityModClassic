using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Calamitas;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.Calamitas
{
	public class CalamitasBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("CalamitasRun3").Type;
		}

		public override bool CanRightClick()
		{
			return true;
		}
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamityRing>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamityDust>(), 1, 14, 18));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BlightedLens>(), 1, 1, 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 4, 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasInferno>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheEyeofCalamitas>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BlightedEyeStaff>(), 3));
        }
	}
}