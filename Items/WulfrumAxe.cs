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
	public class WulfrumAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wulfrum Axe");
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 62;
			Item.height = 58;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useTurn = true;
			Item.axe = 7;
			Item.useStyle = 1;
			Item.knockBack = 4.5f;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "WulfrumShard", 14);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}