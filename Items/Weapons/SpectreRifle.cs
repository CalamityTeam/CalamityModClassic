using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SpectreRifle : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/SpectreRifle");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Spectre Rifle");
        Item.damage = 150;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 68;
        Item.height = 24;
        ////Tooltip.SetDefault("Fires lost souls at a high velocity");
        Item.crit += 22;
        Item.useTime = 33;
        Item.useAnimation = 33;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7;
        Item.value = 550000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shoot = 297; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 24f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int projectile2 = Projectile.NewProjectile(source, position, velocity,297, damage, knockback, player.whoAmI, 0.0f, 0.0f);
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