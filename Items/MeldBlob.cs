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
	public class MeldBlob : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Meld Blob");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 14;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 9;
		}
	}
}