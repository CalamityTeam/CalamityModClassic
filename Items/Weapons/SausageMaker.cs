using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SausageMaker : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/3.Perforators/SausageMaker");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Sausage Maker");
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 29;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.useTime = 20;
		Item.knockBack = 6.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 42;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 105000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("SausageMaker").Type;
		Item.shootSpeed = 4f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "BloodSample", 8);
		recipe.AddIngredient(ItemID.Vertebrae, 4);
        recipe.AddIngredient(ItemID.CrimtaneBar, 5);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
	}
}}
