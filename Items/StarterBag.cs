using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.DifficultyItems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items
{
	public class StarterBag : ModItem
	{
		public override void SetStaticDefaults()
 		{
 			// DisplayName.SetDefault("Starter Bag");
 			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
 		}
		
		public override void SetDefaults()
		{
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 1;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			
			itemLoot.Add(new CommonDrop(ItemID.CopperBroadsword, 1)); //melee needs
			itemLoot.Add(new CommonDrop(ItemID.CopperBow, 1)); //ranged needs
			itemLoot.Add(new CommonDrop(ItemID.WoodenArrow, 1, 100, 100));
			itemLoot.Add(new CommonDrop(ItemID.AmethystStaff, 1)); //mage needs
            itemLoot.Add(new CommonDrop(ItemID.ManaCrystal, 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<WulfrumKnife>(), 1, 150, 150));
            itemLoot.Add(new CommonDrop(ItemID.SlimeStaff, 1)); //summoner needs
            itemLoot.Add(new CommonDrop(ItemID.CopperHammer, 1)); //tool needs
            itemLoot.Add(new CommonDrop(ItemID.MiningPotion, 1)); //mining needs
            itemLoot.Add(new CommonDrop(ItemID.SpelunkerPotion, 1, 2,2 )); //mining/treasure needs
            itemLoot.Add(new CommonDrop(ItemID.SwiftnessPotion, 1, 3, 3)); //movement needs
            itemLoot.Add(new CommonDrop(ItemID.GillsPotion, 1, 2, 2)); //abyss needs
            itemLoot.Add(new CommonDrop(ItemID.ShinePotion, 1)); //mining needs
            itemLoot.Add(new CommonDrop(ItemID.SlimeCrown, 1)); //speedruns lul needs
            itemLoot.Add(new CommonDrop(ItemID.Chest, 1, 3, 3)); //storage needs
            itemLoot.Add(new CommonDrop(ItemID.Torch, 1, 25, 25)); //speedruns lul needs
            itemLoot.Add(new CommonDrop(ItemID.Bomb, 1, 10, 10)); //speedruns lul needs
            itemLoot.Add(new CommonDrop(ModContent.ItemType<Death>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<DefiledRune>(), 1));
			itemLoot.Add(new CommonDrop(Mod.Find<ModItem>("CalamityMusicbox").Type, 1));
		}
	}
}