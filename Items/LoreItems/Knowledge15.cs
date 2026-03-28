using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.LoreItems
{
	public class Knowledge15 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Slime God");
			/* Tooltip.SetDefault("It is a travesty, one of the most threatening biological terrors ever created.\n" +
                "If this creature were allowed to combine every slime on the planet it would become nearly unstoppable.\n" +
				"Place in your inventory to become slimed and able to slide around on tiles quickly."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 4;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			player.slippy2 = true;
			if (Main.myPlayer == player.whoAmI)
			{
				player.AddBuff(BuffID.Slimed, 2);
			}
		}
	}
}