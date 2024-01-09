using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.SlimeGod {
public class TheGodsGambit : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The God's Gambit");
	}

    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Kraken);
        Item.damage = 26;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3.5f;
        Item.value = 300000;
        Item.rare = ItemRarityID.LightRed;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TheGodsGambitProjectile").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 40);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}