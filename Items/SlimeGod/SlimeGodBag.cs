using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Placeables.FurnitureStatigel;
using CalamityModClassicPreTrailer.Items.Weapons.SlimeGod;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.SlimeGod
{
	public class SlimeGodBag : ModItem
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
			itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<ElectrolyteGelPack>(), 1)));
				itemLoot.Add(revActive.OnSuccess(new OneFromOptionsDropRule(20, 1, new int[]
				{
					ModContent.ItemType<StressPills>(),
					ModContent.ItemType<Laudanum>(),
					ModContent.ItemType<HeartofDarkness>(),
				})));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<OverloadedBlaster>(), 3));
			itemLoot.Add(ItemDropRule.OneFromOptions(7, new int[]
			{
				ModContent.ItemType<SlimeGodMask>(),
				ModContent.ItemType<SlimeGodMask2>()
			}));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<AbyssalTome>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EldritchTome>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CrimslimeStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<CorroslimeStaff>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<GelDart>(), 3));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<StaticRefiner>(), 1));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<ManaOverloader>(), 1));
			itemLoot.Add(new CommonDrop(ItemID.Gel, 1, 150, 201));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<PurifiedGel>(), 1, 30, 51));
		}
	}
}