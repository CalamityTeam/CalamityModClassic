using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Shroomer : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shroomer");
	}

    public override void SetDefaults()
    {
        Item.damage = 200;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 96;
        Item.height = 40;
        Item.crit += 35;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 9.75f;
        Item.value = 900000;
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 10f;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-25, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	if (Main.rand.NextBool(5))
    	{
    		Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Shroom").Type, (int)((double)damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}

    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SniperRifle);
        recipe.AddIngredient(ItemID.ShroomiteBar, 11);
        recipe.AddIngredient(ItemID.FragmentVortex, 15);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}