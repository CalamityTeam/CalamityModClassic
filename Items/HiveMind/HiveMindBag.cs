using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Cryogen;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.HiveMind
{
	public class HiveMindBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<HiveMindMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RottenBrain>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.RottenChunk, 1, 10, 16));
            itemLoot.Add(new CommonDrop(ItemID.DemoniteBar, 1, 9, 11));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TrueShadowScale>(), 1, 30, 39));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<LeechingDagger>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ShadowdropStaff>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RotBall>(), 5, 50, 74));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PerfectDark>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Shadethrower>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DankStaff>(), 9));
        }
	}
}