using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories.RareVariants
{
	public class TheTransformer : ModItem
	{
		public override void SetStaticDefaults()
		{
				// DisplayName.SetDefault("The Transformer");
				/* Tooltip.SetDefault("Taking damage releases a blast of sparks\n" +
									"Sparks do extra damage in Hardmode\n" +
									"Immunity to Electrified and you resist all elctrical projectile and enemy damage\n" +
									"Enemy bullets do half damage to you and are reflected back at the enemy for 800% their original damage"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 1;
			Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.aSparkRare = true;
			modPlayer.aSpark = true;
		}
	}
}