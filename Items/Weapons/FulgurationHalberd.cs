﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FulgurationHalberd : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/FulgurationHalberd");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Fulguration Halberd");
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 53;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 22;
		Item.useStyle = 1;
		Item.useTime = 22;
		Item.useTurn = true;
		Item.knockBack = 4.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 60;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 125000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrystalShard, 20);
        recipe.AddIngredient(ItemID.OrichalcumHalberd);
        recipe.AddIngredient(ItemID.HellstoneBar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.CrystalShard, 20);
        recipe.AddIngredient(ItemID.MythrilHalberd);
        recipe.AddIngredient(ItemID.HellstoneBar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}