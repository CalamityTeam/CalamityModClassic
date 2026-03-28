using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.PlaguebringerGoliath
{
	public class PlagueCellCluster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plague Cell Canister");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = 8;
		}
	}
}