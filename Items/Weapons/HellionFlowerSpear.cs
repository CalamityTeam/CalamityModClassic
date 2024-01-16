using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class HellionFlowerSpear : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/HellionFlowerSpear");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Hellion Flower Spear");
		Item.width = 74;  //The width of the .png file in pixels divided by 2.
		Item.damage = 56;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 24;
		Item.useStyle = 5;
		Item.useTime = 24;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 74;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 355000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("HellionFlowerSpearProjectile").Type;
		Item.shootSpeed = 4f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "DraedonBar", 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
