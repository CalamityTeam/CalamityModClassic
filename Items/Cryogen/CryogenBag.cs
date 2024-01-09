using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Cryogen;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.Cryogen
{
	public class CryogenBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("Cryogen").Type;
		}

		public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CryogenMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SoulofCryogen>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.FrostCore, 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CryoBar>(), 1, 20, 40));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofEleum>(), 1, 4, 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<GlacialCrusher>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BittercoldStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<IceStar>(), 3, 150, 200));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EffluviumBow>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Icebreaker>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SnowstormStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Permafrost>(), 3));
        }
	}
}