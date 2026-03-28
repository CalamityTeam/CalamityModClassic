using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Accessories.RareVariants;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage.RareVariants;
using CalamityModClassicPreTrailer.Items.Weapons.AstrumDeus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.AstrumDeus
{
	public class AstrumDeusBag : ModItem
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
			Item.expert = true;
			Item.rare = 9;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<StarlightFuelCell>(), 1)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Quasar>(), 40));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<HideofAstrumDeus>(), 40));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Stardust>(), 1, 60, 91));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Starfall>(), 4));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Nebulash>(), 4));
			itemLoot.Add(new CommonDrop(ItemID.HallowedKey, 5));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<AstralBulwark>(), 1));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<AstrumDeusMask>(), 7));
		}
	}
}