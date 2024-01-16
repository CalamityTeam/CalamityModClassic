using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.TheDevourerofGods;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.Yharon
{
	public class YharonBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<YharonMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<YharimsGift>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<HellcasterFragment>(), 1, 5, 8));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<AngryChickenStaff>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DragonsBreath>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DragonRage>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ProfanedTrident>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PhoenixFlameBarrage>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheBurningSky>(), 4));
        }
	}
}