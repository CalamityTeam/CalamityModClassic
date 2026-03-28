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
	public class Phantoplasm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantoplasm");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 999;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 7, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(200, 200, 200, 0);
		}
	}
}