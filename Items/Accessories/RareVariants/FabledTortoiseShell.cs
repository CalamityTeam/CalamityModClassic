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
	public class FabledTortoiseShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fabled Tortoise Shell");
			/* Tooltip.SetDefault("50% reduced movement speed\n" +
								"Enemies take damage when they hit you\n" +
								"You move quickly for a short time if you take damage"); */
		}
		
		public override void SetDefaults()
		{
			Item.defense = 50;
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.fabledTortoise = true;
			player.moveSpeed -= 0.5f;
			player.thorns = 0.25f;
			player.statDefense += 42;
		}
	}
}