using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Shimmerspark : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shimmerspark");
		//Tooltip.SetDefault("Fires stars when enemies are near");
	}

    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Chik);
        Item.damage = 32;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3.2f;
        Item.value = 100000;
        Item.rare = ItemRarityID.Pink;
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