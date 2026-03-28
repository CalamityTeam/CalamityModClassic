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
	public class TheBee : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Bee");
			/* Tooltip.SetDefault("Causes stars to fall and releases bees when injured\n" +
								"All projectiles gain bonus damage if you are at full HP\n" +
								"The amount of bonus damage is based on your weapon's damage and fire rate\n" +
								"Does not work with summons or sentries"); */
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = Item.buyPrice(0, 9, 0, 0);
			Item.rare = 4;
			Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// thanks the bee very cool
			player.starCloakItem_beeCloakOverrideItem.active = true;
			player.starCloakItem.active = true;
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.theBee = true;
		}
	}
}