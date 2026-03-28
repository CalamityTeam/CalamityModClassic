using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.DesertScourge;
using CalamityModClassicPreTrailer.Items.Weapons.AquaticScourge;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.AquaticScourge
{
	public class AquaticScourgeBag : ModItem
	{
		public override void SetStaticDefaults()
 		{
 			// DisplayName.SetDefault("Treasure Bag");
 			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
 		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 9;
			Item.expert = true;
		}
		
		public override bool CanRightClick()
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
					itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));

			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DeepseaStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Downpour>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<SubmarineShocker>(), 3));
			itemLoot.Add(new CommonDrop(ItemID.HighTestFishingLine, 15));
			itemLoot.Add(new CommonDrop(ItemID.AnglerTackleBag, 30));
			itemLoot.Add(new CommonDrop(ItemID.TackleBox, 15));
			itemLoot.Add(new CommonDrop(ItemID.AnglerEarring, 10));
			itemLoot.Add(new CommonDrop(ItemID.FishermansGuide, 10));
			itemLoot.Add(new CommonDrop(ItemID.HighTestFishingLine, 10));
			itemLoot.Add(new CommonDrop(ItemID.WeatherRadio, 10));
			itemLoot.Add(new CommonDrop(ItemID.Sextant, 10));
			itemLoot.Add(new CommonDrop(ItemID.AnglerHat, 5));
			itemLoot.Add(new CommonDrop(ItemID.AnglerVest, 5));
			itemLoot.Add(new CommonDrop(ItemID.AnglerPants, 5));
			itemLoot.Add(new CommonDrop(ItemID.CratePotion, 5, 2, 4));
			itemLoot.Add(new CommonDrop(ItemID.FishingPotion, 5, 2, 4));
			itemLoot.Add(new CommonDrop(ItemID.SonarPotion, 5, 2, 4));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<AeroStone>(), 10));
			itemLoot.Add(ItemDropRule.ByCondition(new SkeletronCondition(), ItemID.GoldenBugNet, 20));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<AquaticEmblem>(), 1));
            itemLoot.Add(new CommonDrop(ModContent.ItemType<VictoryShard>(), 1, 15, 26));
            itemLoot.Add(new CommonDrop(ItemID.Coral, 1, 7, 12));
            itemLoot.Add(new CommonDrop(ItemID.Seashell, 1, 7, 12));
            itemLoot.Add(new CommonDrop(ItemID.Starfish, 1, 7, 12));
            itemLoot.Add(new CommonDrop(ItemID.SoulofSight, 1, 25, 41));
        }
	}
}