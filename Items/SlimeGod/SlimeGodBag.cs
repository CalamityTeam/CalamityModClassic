using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.SlimeGod;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.SlimeGod
{
	public class SlimeGodBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("SlimeGodRun").Type;
		}

		public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(7, ModContent.ItemType<SlimeGodMask>(), ModContent.ItemType<SlimeGodMask2>()));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ManaOverloader>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.Gel, 1, 100, 125));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PurifiedGel>(), 1, 30, 50));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<GelDart>(), 1, 100, 125));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<OverloadedBlaster>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<AbyssalTome>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EldritchTome>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CrimslimeStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CorroslimeStaff>(), 3));
        }
	}
}