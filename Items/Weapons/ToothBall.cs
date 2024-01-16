using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ToothBall : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/3.Perforators/ToothBall");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Tooth Ball");
		Item.width = 32;  //The width of the .png file in pixels divided by 2.
		Item.damage = 18;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 18;
		Item.crit = 6;
		Item.useStyle = 1;
		Item.useTime = 18;
		Item.knockBack = 2.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 32;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 999;
		Item.value = 1000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("ToothBallProjectile").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(100);
        recipe.AddIngredient(null, "BloodSample");
        recipe.AddIngredient(ItemID.Vertebrae);
        recipe.AddIngredient(ItemID.CrimtaneBar);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
	}
}}
