using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Zapper : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Zapper");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Lazinator");
        Item.damage = 33;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 4;
        Item.width = 46;
        Item.height = 22;
        Item.useTime = 7;
        Item.useAnimation = 7;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Zap");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2f;
        Item.value = 180000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item12;
        Item.autoReuse = true;
        Item.shoot = 88;
        Item.shootSpeed = 20f;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LaserRifle);
        recipe.AddIngredient(null, "VictoryShard", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}