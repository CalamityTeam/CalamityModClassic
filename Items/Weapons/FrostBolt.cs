using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FrostBolt : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/FrostBolt");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Frost Bolt");
        Item.damage = 15;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 6;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts a slow-moving ball of frost");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.5f;
        Item.value = 30000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("FrostBoltProjectile").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.IceBlock, 20);
		recipe.AddIngredient(ItemID.Shiverthorn, 2);
		recipe.AddIngredient(ItemID.SnowBlock, 10);
		recipe.AddIngredient(ItemID.WaterBucket);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
	}
}}