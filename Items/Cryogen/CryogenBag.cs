using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Calamitas;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.Cryogen
{
	public class CryogenBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CryogenMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SoulofCryogen>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.FrostCore, 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CryoBar>(), 1, 20, 39));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofEleum>(), 1, 4, 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<GlacialCrusher>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BittercoldStaff>(), 8));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<IceStar>(), 5, 50, 99));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EffluviumBow>(), 8));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Icebreaker>(), 8));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SnowstormStaff>(), 8));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Permafrost>(), 8));
        }
	}
}