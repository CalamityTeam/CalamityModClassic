using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.DevourerMunsters
{
	public class ArmoredShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Armored Shell");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 7, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	}
}