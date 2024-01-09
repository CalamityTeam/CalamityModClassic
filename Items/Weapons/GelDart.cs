using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class GelDart : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/GelDart");
        return true;
    }

	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Gel Dart");
		Item.width = 14;  //The width of the .png file in pixels divided by 2.
		Item.damage = 23;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 11;
		Item.useStyle = 1;
		Item.useTime = 11;
		Item.knockBack = 2.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 28;  //The height of the .png file in pixels divided by 2.
		Item.shoot = 330;
		Item.maxStack = 999;
		Item.value = 1000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("GelDartProjectile").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(150);
        recipe.AddIngredient(null, "PurifiedGel", 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
