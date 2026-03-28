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
	public class Stardust : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stardust");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 18;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 3, 0, 0);
			Item.rare = 5;
		}
	}
}