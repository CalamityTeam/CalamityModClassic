using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Cryogen;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.DesertScourge
{
	public class DesertScourgeBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DesertScourgeMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<OceanCrest>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.Coral, 1, 7, 11));
            itemLoot.Add(new CommonDrop(ItemID.Seashell, 1, 7, 11));
            itemLoot.Add(new CommonDrop(ItemID.Starfish, 1, 7, 11));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<VictoryShard>(), 1, 10, 16));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SeaboundStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<StormSpray>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Barinade>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<AquaticDischarge>(), 3));
        }
	}
}