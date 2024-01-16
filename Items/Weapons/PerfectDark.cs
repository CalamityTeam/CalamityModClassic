using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PerfectDark : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/2.HiveMind/PerfectDark");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Perfect Dark");
		Item.width = 50;  //The width of the .png file in pixels divided by 2.
		Item.damage = 25;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.useTime = 25;
		Item.useTurn = true;
		Item.knockBack = 4.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 50000;  //Value is calculated in copper coins.
		Item.rare = 4;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("DarkBall").Type;
		Item.shootSpeed = 5f;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RottenChunk, 5);
        recipe.AddIngredient(ItemID.DemoniteBar, 5);
        recipe.AddIngredient(null, "TrueShadowScale", 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}
