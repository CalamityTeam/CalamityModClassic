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
	public class Crystalline : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystalline");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 16;
			Item.crit += 4;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 18;
			Item.knockBack = 3f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
			Item.maxStack = 1;
			Item.value = 52000;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("Crystalline").Type;
			Item.shootSpeed = 10f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.ThrowingKnife, 50);
	        recipe.AddIngredient(ItemID.Diamond, 5);
	        recipe.AddIngredient(ItemID.FallenStar, 3);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
