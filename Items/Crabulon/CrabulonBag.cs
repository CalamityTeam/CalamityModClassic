using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Crabulon;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Crabulon
{
	public class CrabulonBag : ModItem
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
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<MushroomPlasmaRoot>(), 1)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CrabulonMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<HyphaeRod>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<MycelialClaws>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Mycoroot>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Fungicide>(), 3));
			itemLoot.Add(new CommonDrop(ItemID.GlowingMushroom, 1, 25, 36));
			itemLoot.Add(new CommonDrop(ItemID.MushroomGrassSeeds, 1, 5, 11));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<FungalClump>(), 1));
		}
	}
}