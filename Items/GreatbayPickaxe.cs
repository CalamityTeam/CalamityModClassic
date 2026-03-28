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
	public class GreatbayPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Greatbay Pickaxe");
			// Tooltip.SetDefault("Can mine Meteorite");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 44;
			Item.height = 44;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useTurn = true;
			Item.pick = 60;
			Item.useStyle = 1;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "VictideBar", 3);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}