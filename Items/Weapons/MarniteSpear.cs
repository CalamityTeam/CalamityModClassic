﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MarniteSpear : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/MarniteSpear");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Marnite Spear");
		Item.width = 50;  //The width of the .png file in pixels divided by 2.
		Item.damage = 15;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 21;
		Item.useStyle = 5;
		Item.useTime = 21;
		Item.knockBack = 5.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 65000;  //Value is calculated in copper coins.
		Item.rare = 2;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("MarniteSpearProjectile").Type;
		Item.shootSpeed = 4f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PlatinumBar, 5);
		recipe.AddIngredient(ItemID.Granite, 9);
		recipe.AddIngredient(ItemID.Marble, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.GoldBar, 5);
		recipe.AddIngredient(ItemID.Granite, 9);
		recipe.AddIngredient(ItemID.Marble, 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
