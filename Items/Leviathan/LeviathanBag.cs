using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Weapons;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Leviathan
{
	public class LeviathanBag : ModItem
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
			itemLoot.Add(new CommonDrop(ModContent.ItemType<LeviathanMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<LeviathanAmbergris>(), 1));
			itemLoot.Add(new FromOptionsWithoutRepeatsDropRule(1, new int[] { ModContent.ItemType<Atlantis>(), ModContent.ItemType<BrackishFlask>(), ModContent.ItemType<Leviatitan>(), ModContent.ItemType<LureofEnthrallment>(), ModContent.ItemType<SirensSong>(), ModContent.ItemType<Greentide>()}));
        }
	}
}