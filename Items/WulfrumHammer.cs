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
	public class WulfrumHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wulfrum Hammer");
		}

		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 56;
			Item.height = 56;
			Item.useTime = 29;
			Item.useAnimation = 29;
			Item.useTurn = true;
			Item.hammer = 35;
			Item.useStyle = 1;
			Item.knockBack = 5.5f;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "WulfrumShard", 16);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}