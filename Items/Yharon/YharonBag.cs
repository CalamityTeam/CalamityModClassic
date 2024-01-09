using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.Yharon;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassic1Point2.Items.Yharon
{
	public class YharonBag : ModItem
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
			//bossBagNPC = Mod.Find<ModNPC>("Yharon").Type;
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
            itemLoot.Add(new CommonDrop(ModContent.ItemType<AngryChickenStaff>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DragonsBreath>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DragonRage>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ProfanedTrident>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<PhoenixFlameBarrage>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<TheBurningSky>(), 3));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<ChickenCannon>(), 3));
        }
	}
}