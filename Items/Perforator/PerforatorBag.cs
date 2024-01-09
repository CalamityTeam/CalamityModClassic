using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Perforators;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.Perforator
{
	public class PerforatorBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("PerforatorHive").Type;
		}

		public override bool CanRightClick()
		{
			return true;
		}
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PerforatorMask>(), 7));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodyWormTooth>(), 1));
            itemLoot.Add(new CommonDrop(ItemID.Vertebrae, 1, 10, 20));
            itemLoot.Add(new CommonDrop(ItemID.CrimtaneBar, 1, 9, 14));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodSample>(), 1, 10, 20));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodyRupture>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<SausageMaker>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodBath>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ToothBall>(), 3, 50, 75));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<VeinBurster>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Eviscerator>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodClotStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Aorta>(), 3));
        }
	}
}