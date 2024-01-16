using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class GoldplumeSpear : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/GoldplumeSpear");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Goldplume Spear");
		Item.width = 54;  //The width of the .png file in pixels divided by 2.
		Item.damage = 23;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 23;
		Item.useStyle = 5;
		Item.useTime = 23;
		Item.knockBack = 5.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 54;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 85000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("GoldplumeSpearProjectile").Type;
		Item.shootSpeed = 4f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "AerialiteBar", 10);
		recipe.AddIngredient(ItemID.SunplateBlock, 4);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
	}
}}
