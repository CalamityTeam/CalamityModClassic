using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Leviathan
{
	public class IOU : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("IOU an item");
			// Tooltip.SetDefault("Use to craft any Leviathan weapon you want\nCombine with Living Shards from Plantera to get your item!");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = 1;
		}
	}
}