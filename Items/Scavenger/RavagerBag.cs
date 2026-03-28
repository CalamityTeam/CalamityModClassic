using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons.Ravager;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Scavenger
{
	public class RavagerBag : ModItem
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
			LeadingConditionRule PreProv = new LeadingConditionRule(new ProvCondition());
				itemLoot.Add(revActive.OnSuccess(new CommonDrop(ModContent.ItemType<InfernalBlood>(), 1)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			Main.LocalPlayer.TryGettingDevArmor(null);
			if (CalamityWorldPreTrailer.downedProvidence)
			{
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Bloodstone>(), 1, 60, 71));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<VerstaltiteBar>(), 1, 7, 13));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<DraedonBar>(), 1, 7, 13));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CruptixBar>(), 1, 7, 13));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofCinder>(), 1, 2, 5));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofEleum>(), 1, 2, 5));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofChaos>(), 1, 2, 5));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<BarofLife>(), 1));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<CoreofCalamity>(), 2));
				itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<BloodflareCore>(), 1));
			}
			else
			{
				itemLoot.Add(PreProv.OnFailedConditions(new CommonDrop(ModContent.ItemType<VerstaltiteBar>(), 1, 2, 4)));
				itemLoot.Add(PreProv.OnFailedConditions(new CommonDrop(ModContent.ItemType<DraedonBar>(), 1, 2, 4)));
				itemLoot.Add(PreProv.OnFailedConditions(new CommonDrop(ModContent.ItemType<CruptixBar>(), 1, 2, 4)));
				itemLoot.Add(PreProv.OnFailedConditions(new CommonDrop(ModContent.ItemType<CoreofCinder>(), 1, 1, 4)));
				itemLoot.Add(PreProv.OnFailedConditions(new CommonDrop(ModContent.ItemType<CoreofEleum>(), 1, 1, 4)));
				itemLoot.Add(PreProv.OnFailedConditions(new CommonDrop(ModContent.ItemType<CoreofChaos>(), 1, 1, 4)));
			}
			itemLoot.Add(new CommonDrop(ModContent.ItemType<BloodPact>(), 2));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<FleshTotem>(), 2));
			itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[]
			{
				ModContent.ItemType<Hematemesis>(),
				ModContent.ItemType<RealmRavager>(),
				ModContent.ItemType<SpikecragStaff>(),
				ModContent.ItemType<UltimusCleaver>(),
				ModContent.ItemType<CraniumSmasher>(),
			}));
		}
	}
}