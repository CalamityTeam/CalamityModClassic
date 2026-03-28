using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
	public class BlightedLens : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blighted Lens");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 5;
		}
	}
}