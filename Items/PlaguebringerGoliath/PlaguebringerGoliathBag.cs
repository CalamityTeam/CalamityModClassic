using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Plaguebringer;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.PlaguebringerGoliath
{
	public class PlaguebringerGoliathBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("PlaguebringerGoliath").Type;
		}

		public override bool CanRightClick()
		{
			return true;
		}
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PlaguebringerGoliathMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ToxicHeart>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PlagueCellCluster>(), 1, 13, 17));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PestilentDefiler>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ThePlaguebringer>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheHive>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Malevolence>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DiseasedPike>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<VirulentKatana>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<MepheticSprayer>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PlagueStaff>(), 3));
        }
	}
}