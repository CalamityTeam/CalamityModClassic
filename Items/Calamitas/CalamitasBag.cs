using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Leviathan;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.Calamitas
{
	public class CalamitasBag : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Treasure Bag");
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			////Tooltip.SetDefault("Right click to open");
			Item.rare = 9;
			Item.expert = true;
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BlightedLens>(), 1, 1, 2));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 4, 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasInferno>(), 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheEyeofCalamitas>(), 14));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BlightedEyeStaff>(), 15));
        }
	}
}