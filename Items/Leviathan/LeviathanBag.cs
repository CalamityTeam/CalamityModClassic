using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Leviathan;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.Leviathan
{
	public class LeviathanBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("Siren").Type;
		}

		public override bool CanRightClick()
		{
			return true;
		}
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<LeviathanMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<LeviathanAmbergris>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Atlantis>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Greentide>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BrackishFlask>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Leviatitan>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<LureofEnthrallment>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SirensSong>(), 3));
		}
	}
}