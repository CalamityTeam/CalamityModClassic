using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.Weapons;
using CalamityModClassicPreTrailer.Items.Weapons.Calamitas;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Calamitas
{
	public class CalamitasBag : ModItem
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
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<Animosity>(), 1)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasInferno>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamitasMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TheEyeofCalamitas>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BlightedEyeStaff>(), 3));
			itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Bloodstone>(), 1, 35, 46));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ChaosStone>(), 10));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamityDust>(), 1, 14, 19));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 5, 10));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BlightedLens>(), 1, 1, 4));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CalamityRing>(), 1));
		}
	}
}