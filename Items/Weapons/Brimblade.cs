using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Brimblade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Brimblade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Brimblade");
		Item.width = 26;  //The width of the .png file in pixels divided by 2.
		Item.damage = 60;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.useTime = 15;
		////Tooltip.SetDefault("Throws a brimstone blade that splits into more blades on enemy hits");
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 26;  //The height of the .png file in pixels divided by 2.
		Item.value = 300000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Brimblade").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UnholyCore", 4);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
