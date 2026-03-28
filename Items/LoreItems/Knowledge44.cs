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
	public class Knowledge44 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jungle Dragon, Yharon");
			/* Tooltip.SetDefault("I would not be able to bear a world without my faithful companion by my side.\n" +
				"Fortunately, fate will have it so that it is a world I shall never have to see, for better or for worse.\n" +
				"Place in your inventory to gain nearly-infinite wing flight time but at the cost of a 25% decrease to all damage."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.yharonLore = true;
		}
	}
}