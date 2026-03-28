using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Perforators;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Perforator
{
	public class PerforatorBag : ModItem
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
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PerforatorMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Aorta>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodyRupture>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<SausageMaker>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodBath>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<VeinBurster>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Eviscerator>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<VeinBurster>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Eviscerator>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ToothBall>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodClotStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodyWormTooth>(), 1));
			itemLoot.Add(new CommonDrop(ItemID.Vertebrae, 1, 10, 21));
			itemLoot.Add(new CommonDrop(ItemID.CrimtaneBar, 1, 9, 15));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodSample>(), 1, 30, 41));
			itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Ichor, 1, 15, 31));
		}
	}
}