using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Perforators
{
	public class ToothBall : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tooth Ball");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.damage = 20;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 13;
			Item.crit = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 13;
			Item.knockBack = 2.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = 1000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("ToothBallProjectile").Type;
			Item.shootSpeed = 16f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
	        recipe.AddIngredient(null, "BloodSample");
	        recipe.AddIngredient(ItemID.Vertebrae);
	        recipe.AddIngredient(ItemID.CrimtaneBar);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
		}
	}
}
