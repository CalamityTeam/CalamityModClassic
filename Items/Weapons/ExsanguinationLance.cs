using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class ExsanguinationLance : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/ExsanguinationLance");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Exsanguination Lance");
		Item.width = 90;  //The width of the .png file in pixels divided by 2.
		Item.damage = 62;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 29;
		Item.useStyle = 5;
		Item.useTime = 29;
		Item.knockBack = 5.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 90;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 385000;  //Value is calculated in copper coins.
		Item.rare = 7;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("ExsanguinationLanceProjectile").Type;
		Item.shootSpeed = 5f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 10);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}
}}
