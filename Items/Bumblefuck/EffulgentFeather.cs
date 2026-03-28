using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Bumblefuck
{
	public class EffulgentFeather : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Effulgent Feather");
			// Tooltip.SetDefault("It vibrates with fluffy golden energy");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 12));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 7, 50, 0);
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	}
}