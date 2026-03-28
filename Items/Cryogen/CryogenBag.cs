using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Accessories.RareVariants;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Cryogen;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Cryogen
{
	public class CryogenBag : ModItem
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
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<FrostFlare>(), 1)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CryogenMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<GlacialCrusher>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Avalanche>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BittercoldStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<IceStar>(), 3, 150, 201));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EffluviumBow>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Icebreaker>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<SnowstormStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Regenator>(), 40));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CryoStone>(), 10));
			itemLoot.Add(new CommonDrop(ItemID.SoulofMight, 1, 25, 41));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CryoBar>(), 1, 20, 41));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofEleum>(), 1, 5, 10));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<SoulofCryogen>(), 1));
			itemLoot.Add(new CommonDrop(ItemID.FrostCore, 1));
		}
	}
}