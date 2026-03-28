using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Plaguebringer;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.PlaguebringerGoliath
{
	public class PlaguebringerGoliathBag : ModItem
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
			itemLoot.Add(revActive.OnSuccess(ItemDropRule.ByCondition(new DefiledCondition(), ModContent.ItemType<Malachite>(), 20)));
			itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<Malachite>(), 100)));
				itemLoot.Add(revActive.OnSuccess(new OneFromOptionsDropRule(20, 1, new int[]
				{ 
					ModContent.ItemType<StressPills>(),
					ModContent.ItemType<Laudanum>(),
					ModContent.ItemType<HeartofDarkness>(),
				})));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PestilentDefiler>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ThePlaguebringer>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PlaguebringerGoliathMask>(), 7));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<TheHive>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Malevolence>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<DiseasedPike>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<VirulentKatana>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<MepheticSprayer>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PlagueStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloomStone>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ToxicHeart>(), 1));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PlagueCellCluster>(), 1, 13, 18));
		}
	}
}