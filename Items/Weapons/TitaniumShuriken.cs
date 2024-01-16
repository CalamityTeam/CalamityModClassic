using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TitaniumShuriken : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/TitaniumShuriken");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Titanium Shuriken");
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 31;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 9;
		Item.scale = 0.75f;
		Item.crit = 10;
		Item.useStyle = 1;
		Item.useTime = 9;
		Item.knockBack = 3f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 38;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 999;
		Item.value = 2000;  //Value is calculated in copper coins.
		Item.rare = 4;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TitaniumShurikenProjectile").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(30);
        recipe.AddIngredient(ItemID.TitaniumBar);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
