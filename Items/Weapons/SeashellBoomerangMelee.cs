using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SeashellBoomerangMelee : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Seashell Boomerang");
	}

	public override void SetDefaults()
	{
		Item.width = 18;  //The width of the .png file in pixels divided by 2.
		Item.damage = 15;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.useAnimation = 11;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 11;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 34;  //The height of the .png file in pixels divided by 2.
		Item.value = 50000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Green;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("SeashellBoomerangProjectileMelee").Type;
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
