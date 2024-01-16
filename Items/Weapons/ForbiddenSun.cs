using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ForbiddenSun : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ForbiddenSun");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Forbidden Sun");
        Item.damage = 72;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 33;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts a large ball of fire that explodes on contact");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7f;
        Item.value = 500000;
        Item.rare = 7;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ForbiddenSunProjectile").Type;
        Item.shootSpeed = 9f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CruptixBar", 6);
		recipe.AddIngredient(ItemID.LivingFireBlock, 50);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
	}
}}