using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.DevourerofGods;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.TheDevourerofGods
{
	public class DevourerofGodsBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("DevourerofGodsHead").Type;
		}

		public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DevourerofGodsMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<NebulousCore>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CosmiliteBar>(), 1, 30, 39));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DeathhailStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Eradicator>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Excelsus>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheObliterator>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EradicatorMelee>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Deathwind>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<StaffoftheMechworm>(), 3));
        }
	}
}