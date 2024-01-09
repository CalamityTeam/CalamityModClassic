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
	public class PerfectDark : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Perfect Dark");
		}
	
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.knockBack = 4.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 50;
			Item.value = 50000;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("DarkBall").Type;
			Item.shootSpeed = 5f;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RottenChunk, 5);
	        recipe.AddIngredient(ItemID.DemoniteBar, 5);
	        recipe.AddIngredient(null, "TrueShadowScale", 15);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
	    }
	}
}
