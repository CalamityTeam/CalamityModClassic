using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Weapons.BrimstoneWaifu;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.BrimstoneWaifu
{
	public class BrimstoneWaifuBag : ModItem
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
			revActive.OnSuccess(new CommonDrop(ModContent.ItemType<CharredRelic>(), 1, 1, 1, 1));
			revActive.OnSuccess(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Brimrose>(), 1, 1, 1, 1));
			revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
			{
				ModContent.ItemType<StressPills>(),
				ModContent.ItemType<Laudanum>(),
				ModContent.ItemType<HeartofDarkness>(),
			}))); 
			itemLoot.Add(revActive);
			
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Abaddon>(), 1));
			ItemDropRule.OneFromOptions(1, new int[]
			{
				ModContent.ItemType<Brimlance>(),
				ModContent.ItemType<SeethingDischarge>()
			});
			itemLoot.Add(ItemDropRule.ByCondition(new ProvCondition(), ModContent.ItemType<Bloodstone>(), 1, 25, 36));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<RoseStone>(), 10));
			itemLoot.Add(new CommonDrop(ItemID.SoulofFright, 1, 25, 41));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1, 5, 10));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Gehenna>(), 1));
		}
	}
}