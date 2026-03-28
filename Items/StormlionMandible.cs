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
	public class StormlionMandible : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stormlion Mandible");
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 1;
		}
	}
}