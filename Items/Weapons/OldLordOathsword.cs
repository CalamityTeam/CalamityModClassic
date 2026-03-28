using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class OldLordOathsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Old Lord Oathsword");
			// Tooltip.SetDefault("A relic of the ancient underworld");
		}

		public override void SetDefaults()
		{
			Item.damage = 31;
			Item.width = 78;
			Item.height = 78;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useStyle = 1;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.knockBack = 4.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
		}
	}
}
