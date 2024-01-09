using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class StarnightLance : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Starnight Lance");
	}

	public override void SetDefaults()
	{
		Item.width = 72;  //The width of the .png file in pixels divided by 2.
		Item.damage = 50;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 23;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 23;
		Item.knockBack = 6;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 72;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 325000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("StarnightLanceProjectile").Type;
		Item.shootSpeed = 6f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VerstaltiteBar", 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
