using CalamityModClassicPreTrailer.Items.Accessories;
using CalamityModClassicPreTrailer.Items.Armor;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassicPreTrailer.Items.Astrageldon
{
	public class AstrageldonBag : ModItem
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
			Item.rare = ItemRarityID.Cyan;
		}

		public override bool CanRightClick()
		{
			return true;
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
	        LeadingConditionRule revActive = new LeadingConditionRule(new RevCondition());
	        itemLoot.Add(revActive.OnSuccess(ItemDropRule.ByCondition(new MoonCondition(), ModContent.ItemType<SquishyBeanMount>(), 1)));
				itemLoot.Add(revActive.OnSuccess((new OneFromOptionsDropRule(20, 1, new int[]
					{
						ModContent.ItemType<StressPills>(),
						ModContent.ItemType<Laudanum>(),
						ModContent.ItemType<HeartofDarkness>(),
					}))));
			Main.LocalPlayer.TryGettingDevArmor(null);
			itemLoot.Add(new CommonDrop(ModContent.ItemType<AstralJelly>(), 1,12, 17));
			itemLoot.Add(new CommonDrop(ModContent.ItemType<Stardust>(), 1, 30, 41));
			itemLoot.Add(new CommonDrop(ItemID.FallenStar, 1, 30, 51));
	        itemLoot.Add(new CommonDrop(ModContent.ItemType<AureusMask>(), 7));
        }
	}
}