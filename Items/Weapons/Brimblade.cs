using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class Brimblade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimblade");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.damage = 37;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 18;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Throwing;
			Item.height = 26;
			Item.value = 300000;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("Brimblade").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 4);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
