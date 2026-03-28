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
	public class ReefclawHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reefclaw Hamaxe");
		}

		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 44;
			Item.height = 44;
			Item.useTime = 29;
			Item.useAnimation = 29;
			Item.useTurn = true;
			Item.axe = 13;
			Item.hammer = 50;
			Item.useStyle = 1;
			Item.knockBack = 6f;
			Item.value = Item.buyPrice(0, 2, 0, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "VictideBar", 2);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}