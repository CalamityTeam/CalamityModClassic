using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Accessories.RareVariants;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.DevourerMunsters;
using CalamityModClassicPreTrailer.Items.Weapons.Providence;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Providence
{
	public class ProvidenceBag : ModItem
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
				itemLoot.Add(revActive.OnSuccess(new OneFromOptionsDropRule(20, 1, new int[]
				{
					ModContent.ItemType<StressPills>(),
					ModContent.ItemType<Laudanum>(),
					ModContent.ItemType<HeartofDarkness>(),
				})));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BlissfulBombardier>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<HolyCollider>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<MoltenAmputator>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PurgeGuzzler>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<SolarFlare>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TelluricGlare>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<SamuraiBadge>(), 40));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ProvidenceMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 25, 36));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DivineGeode>(), 1, 15, 26));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<RuneofCos>(), 1));
		}
	}
}