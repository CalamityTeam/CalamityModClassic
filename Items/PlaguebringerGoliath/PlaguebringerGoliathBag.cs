using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.HiveMind;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.PlaguebringerGoliath
{
	public class PlaguebringerGoliathBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PlaguebringerGoliathMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ToxicHeart>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PlagueCellCluster>(), 1, 13, 17));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PestilentDefiler>(), 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ThePlaguebringer>(), 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheHive>(), 8));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Malevolence>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DiseasedPike>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<VirulentKatana>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<MepheticSprayer>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PlagueStaff>(), 10));
        }
	}
}