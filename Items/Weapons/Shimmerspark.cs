using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Shimmerspark : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Shimmerspark");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Chik);
        //Tooltip.SetDefault("Shimmerspark");
        Item.damage = 37;
        Item.useTime = 25;
        ////Tooltip.SetDefault("Fires stars when enemies are near");
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3.2f;
        Item.value = 100000;
        Item.rare = 5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ShimmersparkProjectile").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 6);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}