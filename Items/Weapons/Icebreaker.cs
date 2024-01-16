using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Icebreaker : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/5.Cryogen/Icebreaker");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Icebreaker");
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 56;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.useAnimation = 16;
		Item.useStyle = 1;
		Item.useTime = 16;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 60;  //The height of the .png file in pixels divided by 2.
		Item.value = 400000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Icebreaker").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CryoBar", 11);
        recipe.AddTile(TileID.IceMachine);
        recipe.Register();
	}
}}
