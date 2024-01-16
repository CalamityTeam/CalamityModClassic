using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MangroveChakram : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/MangroveChakram");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Mangrove Chakram");
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 63;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.useAnimation = 11;
		Item.useStyle = 1;
		Item.useTime = 11;
		////Tooltip.SetDefault("Shred 'em");
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 38;  //The height of the .png file in pixels divided by 2.
		Item.value = 500000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("MangroveChakramProjectile").Type;
		Item.shootSpeed = 15.5f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
