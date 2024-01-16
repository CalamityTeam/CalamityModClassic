using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AccretionDisk : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/AccretionDisk");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Elemental Disk");
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 145;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.useTime = 15;
		////Tooltip.SetDefault("Shred the fabric of reality!");
		Item.knockBack = 9f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 38;  //The height of the .png file in pixels divided by 2.
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("AccretionDisk").Type;
		Item.shootSpeed = 25f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "MangroveChakram");
		recipe.AddIngredient(null, "FlameScythe");
		recipe.AddIngredient(null, "SeashellBoomerang");
		recipe.AddIngredient(null, "GalacticaSingularity", 5);
		recipe.AddIngredient(null, "BarofLife", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
