using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
	public class FungalCarapace : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fungal Carapace");
			// Tooltip.SetDefault("You emit a mushroom spore explosion when you are hit");
		}
		
		public override void SetDefaults()
		{
			Item.defense = 2;
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.fCarapace = true;
		}
	}
}