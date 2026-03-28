using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Perforator
{
	public class BloodSample : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blood Sample");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 22;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 50, 0);
			Item.rare = 2;
		}
	}
}