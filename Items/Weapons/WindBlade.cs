using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class WindBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Wind Blade");
	}

	public override void SetDefaults()
	{
		Item.width = 42;  //The width of the .png file in pixels divided by 2.
		Item.damage = 29;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 20;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 20;
		Item.useTurn = true;
		Item.knockBack = 5;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 42;  //The height of the .png file in pixels divided by 2.
		Item.value = 100000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Orange;  //Ranges from 1 to 11.
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 9);
        recipe.AddIngredient(ItemID.SunplateBlock, 3);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BlueTorch);
        }
    }
}}
