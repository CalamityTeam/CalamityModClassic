using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SpectreRifle : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spectre Rifle");
	}

    public override void SetDefaults()
    {
        Item.damage = 150;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 68;
        Item.height = 24;
        Item.crit += 22;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 7;
        Item.value = 550000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shoot = ProjectileID.LostSoulFriendly;
        Item.shootSpeed = 24f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int projectile2 = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, 297, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		Main.projectile[projectile2].DamageType = DamageClass.Ranged;
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SpectreBar, 7);
        recipe.AddIngredient(null, "CoreofEleum", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}