using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ExecutionersBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/ExecutionersBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Executioner's Blade");
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 215;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useTime = 3;
		Item.useAnimation = 9;
		Item.useStyle = 1;
		Item.knockBack = 6.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item73;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 44;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1350000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("ExecutionersBlade").Type;
		Item.shootSpeed = 26f;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 11);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}
