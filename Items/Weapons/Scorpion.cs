using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Scorpion : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Scorpion");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Scorpio");
        Item.damage = 83;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 58;
        Item.height = 26;
        ////Tooltip.SetDefault("Rockets");
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 3050000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shootSpeed = 20f;
        Item.shoot = Mod.Find<ModProjectile>("MiniRocket").Type;
        Item.useAmmo = 771;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("MiniRocket").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SnowmanCannon);
        recipe.AddIngredient(ItemID.GrenadeLauncher);
        recipe.AddIngredient(ItemID.RocketLauncher);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}