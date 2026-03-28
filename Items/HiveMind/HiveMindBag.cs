using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.HiveMind;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.HiveMind
{
	public class HiveMindBag : ModItem
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
			Item.rare = ItemRarityID.Cyan;
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
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ShaderainStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<LeechingDagger>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ShadowdropStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<HiveMindMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PerfectDark>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Shadethrower>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<RotBall>(), 3, 50, 76));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DankStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<RottenBrain>(),1));
			itemLoot.Add(new CommonDrop(ItemID.RottenChunk, 1, 10, 21));
			itemLoot.Add(new CommonDrop(ItemID.RottenChunk, 1, 9, 15));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TrueShadowScale>(),1, 30, 41));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.CursedFlame, 1, 15, 31));
		}
	}
}