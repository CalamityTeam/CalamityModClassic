using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Perforators
{
	public class SausageMaker : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sausage Maker");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 29;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.useTime = 20;
			Item.knockBack = 6.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 42;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("SausageMaker").Type;
			Item.shootSpeed = 6f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "BloodSample", 8);
			recipe.AddIngredient(ItemID.Vertebrae, 4);
	        recipe.AddIngredient(ItemID.CrimtaneBar, 5);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
		}
	}
}
