using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SpatialLance : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Elemental Lance");
		//Tooltip.SetDefault("Rend the cosmos asunder!");
	}

	public override void SetDefaults()
	{
		Item.width = 88;
		Item.damage = 190;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 18;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 18;
		Item.knockBack = 9.5f;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.height = 88;
		Item.value = 10000000;
		Item.rare = ItemRarityID.Red;
		Item.shoot = Mod.Find<ModProjectile>("SpatialLanceProjectile").Type;
		Item.shootSpeed = 12f;
	}
	
	public override bool CanUseItem(Player player)
    {
        for (int i = 0; i < 1000; ++i)
        {
            if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
            {
                return false;
            }
        }
        return true;
    }

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TerraLance");
		recipe.AddIngredient(ItemID.NorthPole);
		recipe.AddIngredient(null, "GalacticaSingularity", 5);
		recipe.AddIngredient(ItemID.LunarBar, 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}
