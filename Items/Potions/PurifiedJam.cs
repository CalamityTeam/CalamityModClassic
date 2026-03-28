using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Potions
{
	public class PurifiedJam : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Purified Jam");
			/* Tooltip.SetDefault("Makes you immune to all damage and most debuffs for 10 seconds (5 seconds in Death Mode)\n" +
			   "Causes potion sickness when consumed\n" +
			   "Cannot be consumed while potion sickness is active\n" +
			   "Revengeance drop"); */
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 18;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.rare = 3;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}

		public override bool CanUseItem(Player player)
		{
			return player.FindBuffIndex(BuffID.PotionSickness) == -1;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			player.AddBuff(Mod.Find<ModBuff>("Invincible").Type, (CalamityWorldPreTrailer.death ? 300 : 600));
			player.AddBuff(BuffID.PotionSickness, (player.pStone ? 1500 : 1800));
			return true;
		}
	}
}