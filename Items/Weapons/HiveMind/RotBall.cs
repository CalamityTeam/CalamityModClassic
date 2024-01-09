using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.HiveMind
{
	public class RotBall : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rot Ball");
		}
		
		public override void SetDefaults()
		{
			Item.width = 26;
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
			Item.height = 26;
			Item.maxStack = 999;
			Item.value = 1000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("RotBallProjectile").Type;
			Item.shootSpeed = 16f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
	        recipe.AddIngredient(null, "TrueShadowScale");
	        recipe.AddIngredient(ItemID.RottenChunk);
	        recipe.AddIngredient(ItemID.DemoniteBar);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
		}
	}
}
