using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FrigidflashBolt : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/FrigidflashBolt");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Frigidflash Bolt");
        Item.damage = 55;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 13;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts a slow-moving ball of flash-freezing magma");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 500000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item21;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("FrigidflashBoltProjectile").Type;
        Item.shootSpeed = 6.5f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "FrostBolt");
		recipe.AddIngredient(null, "FlareBolt");
		recipe.AddIngredient(null, "EssenceofEleum", 2);
		recipe.AddIngredient(null, "EssenceofChaos", 2);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
	}
}}