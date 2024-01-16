using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.HiveMind;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.DevourerMunsters;

namespace CalamityModClassic1Point1.Items.Providence
{
	public class ProvidenceBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ProvidenceMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<RuneofCos>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 25, 34));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BlissfulBombardier>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<HolyCollider>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<MoltenAmputator>(), 4));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PurgeGuzzler>(), 4));
        }
	}
}