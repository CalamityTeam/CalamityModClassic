using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Leviathan;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Leviathan
{
	public class LeviathanBag : ModItem
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
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<TheCommunity>(), 100)));
				itemLoot.Add(revActive.OnSuccess(ItemDropRule.ByCondition(new DefiledCondition(), ModContent.ItemType<TheCommunity>(), 20)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EnchantedPearl>(), 1));
			itemLoot.Add(new CommonDrop(ItemID.HotlineFishingHook, 10));
			itemLoot.Add(new CommonDrop(ItemID.BottomlessBucket, 10));
			itemLoot.Add(new CommonDrop(ItemID.SuperAbsorbantSponge, 10));
			itemLoot.Add(new CommonDrop(ItemID.CratePotion, 5, 5, 9));
			itemLoot.Add(new CommonDrop(ItemID.SonarPotion, 5, 5, 9));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ModContent.ItemType<IOU>(), 1));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<LeviathanMask>(), 7));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<Atlantis>(), 3)); 
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<BrackishFlask>(), 3));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<Leviatitan>(), 3));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<LureofEnthrallment>(), 3));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<SirensSong>(), 3));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<Greentide>(), 3));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<LeviathanAmbergris>(), 1));
		}
	}
}