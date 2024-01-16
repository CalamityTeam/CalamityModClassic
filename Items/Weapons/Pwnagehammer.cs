using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Pwnagehammer : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Pwnagehammer");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Pwnagehammer");
		Item.width = 68;  //The width of the .png file in pixels divided by 2.
		Item.damage = 68;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 18;
		Item.useStyle = 1;
		Item.useTime = 18;
		////Tooltip.SetDefault("Pure Ownage");
		Item.knockBack = 10f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 68;  //The height of the .png file in pixels divided by 2.
		Item.value = 300000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Pwnagehammer").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Pwnhammer);
		recipe.AddIngredient(ItemID.SoulofMight, 10);
		recipe.AddIngredient(ItemID.HallowedBar, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
