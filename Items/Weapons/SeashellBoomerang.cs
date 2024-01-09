using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SeashellBoomerang : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Seashell Boomerang");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.damage = 15;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 15;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 15;
		Item.knockBack = 5.5f;
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;
		Item.height = 34;
		Item.value = 50000;
		Item.rare = ItemRarityID.Green;
		Item.shoot = Mod.Find<ModProjectile>("SeashellBoomerangProjectile").Type;
		Item.shootSpeed = 11.5f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
