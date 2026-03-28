using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Polterghast;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Polterghast
{
	public class PolterghastBag : ModItem
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
			itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<Ectoheart>(), 1)));
				itemLoot.Add(revActive.OnSuccess(new OneFromOptionsDropRule(20, 1, new int[]
				{
					ModContent.ItemType<StressPills>(),
					ModContent.ItemType<Laudanum>(),
					ModContent.ItemType<HeartofDarkness>(),
				})));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BansheeHook>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DaemonsFlame>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EtherealSubjugator>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<FatesReveal>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<GhastlyVisage>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<GhoulishGouger>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TerrorBlade>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<RuinousSoul>(), 1, 6, 11));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Affliction>(), 1));
		}
	}
}