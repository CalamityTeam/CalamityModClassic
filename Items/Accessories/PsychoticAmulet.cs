using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
	public class PsychoticAmulet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Psychotic Amulet");
			/* Tooltip.SetDefault("Boosts rogue and ranged damage and critical strike chance by 5%\n" +
							   "Grants a massive boost to these stats if you aren't moving"); */
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.value = Item.buyPrice(0, 15, 0, 0);
			Item.rare = 6;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			CalamityCustomThrowingDamagePlayer modPlayer2 = CalamityCustomThrowingDamagePlayer.ModPlayer(player);
			modPlayer.pAmulet = true;
			player.shroomiteStealth = true;
			modPlayer2.throwingDamage += 0.05f;
			modPlayer2.throwingCrit += 5;
			player.GetDamage(DamageClass.Ranged) += 0.05f;
			player.GetCritChance(DamageClass.Ranged) += 5;
		}
	}
}