﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AdamantiteThrowingAxe : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/AdamantiteThrowingAxe");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Adamantite Throwing Axe");
		Item.width = 26;  //The width of the .png file in pixels divided by 2.
		Item.damage = 32;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.useTime = 15;
		Item.knockBack = 3f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 30;  //The height of the .png file in pixels divided by 2.
		Item.shoot = 330;
		Item.maxStack = 999;
		Item.value = 1600;  //Value is calculated in copper coins.
		Item.rare = 4;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("AdamantiteThrowingAxeProjectile").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(25);
        recipe.AddIngredient(ItemID.AdamantiteBar);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
