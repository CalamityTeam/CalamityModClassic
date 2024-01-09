using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.DevourerMunsters;
using CalamityModClassic1Point2.Items.Weapons.Providence;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.Providence
{
	public class ProvidenceBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("Providence").Type;
		}

		public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ProvidenceMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RuneofCos>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 25, 35));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BlissfulBombardier>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<HolyCollider>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<MoltenAmputator>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PurgeGuzzler>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SolarFlare>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TelluricGlare>(), 3));
        }
	}
}