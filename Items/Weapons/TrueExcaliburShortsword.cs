using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TrueExcaliburShortsword : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/TrueExcaliburShortsword");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("True Excalibur Shortsword");
		Item.useStyle = 3;
		Item.useTurn = false;
		Item.useAnimation = 10;
		Item.useTime = 10;  //Ranges from 1 to 55.
		Item.width = 50;  //The width of the .png file in pixels divided by 2.
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.damage = 60;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 5.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.maxStack = 1;
		////Tooltip.SetDefault("Don't underestimate the power of shortswords");
		Item.shoot = Mod.Find<ModProjectile>("ShortBeam").Type;
		Item.shootSpeed = 10f;
		Item.value = 1000000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "ExcaliburShortsword");
		recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 57);
        }
    }
}}
