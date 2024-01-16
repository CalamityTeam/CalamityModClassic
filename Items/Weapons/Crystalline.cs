using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Crystalline : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Crystalline");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Crystalline");
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 14;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 18;
		Item.useStyle = 1;
		Item.useTime = 18;
		Item.knockBack = 3f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 44;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 52000;  //Value is calculated in copper coins.
		Item.rare = 2;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Crystalline").Type;
		Item.shootSpeed = 10f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ThrowingKnife, 50);
        recipe.AddIngredient(ItemID.Diamond, 5);
        recipe.AddIngredient(ItemID.FallenStar, 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
