using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.Calamitas;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.SlimeGod
{
	public class SlimeGodBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SlimeGodMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ManaOverloader>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.Gel, 1, 150, 199));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PurifiedGel>(), 1, 30, 49));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<GelDart>(), 1, 100, 124));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<OverloadedBlaster>(), 2));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<AbyssalTome>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<EldritchTome>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CrimslimeStaff>(), 5));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<CorroslimeStaff>(), 5));
        }
	}
}