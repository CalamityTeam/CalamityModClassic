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
	public class WulfrumPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wulfrum Pickaxe");
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useTurn = true;
			Item.pick = 35;
			Item.useStyle = 1;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "WulfrumShard", 12);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}