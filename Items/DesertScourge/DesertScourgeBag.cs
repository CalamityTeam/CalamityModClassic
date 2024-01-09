using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.DesertScourge;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.DesertScourge
{
	public class DesertScourgeBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("DesertScourgeHead").Type;
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