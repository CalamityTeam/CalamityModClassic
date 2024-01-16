using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class FlareBolt : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/FlareBolt");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Flare Bolt");
        Item.damage = 30;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 12;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts a slow-moving ball of flame");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 90000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("FlareBoltProjectile").Type;
        Item.shootSpeed = 7.5f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.HellstoneBar, 6);
		recipe.AddIngredient(ItemID.Obsidian, 9);
		recipe.AddIngredient(ItemID.Fireblossom, 2);
		recipe.AddIngredient(ItemID.LavaBucket);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
	}
}}