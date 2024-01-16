using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point1.Items.Armor;
using CalamityModClassic1Point1.Items.HiveMind;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point1.Items.Perforator
{
	public class PerforatorBag : ModItem
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PerforatorMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodyWormTooth>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.Vertebrae, 1, 7, 8));
            itemLoot.Add(new CommonDrop(ItemID.CrimtaneBar, 1, 5, 6));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodSample>(), 1, 12, 24));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodyRupture>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SausageMaker>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodBath>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ToothBall>(), 5, 50, 74));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<VeinBurster>(), 10));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Eviscerator>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodClotStaff>(), 9));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Aorta>(), 9));
        }
	}
}