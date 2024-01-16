using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Aeries : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Aeries");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Aeries");
        Item.damage = 32;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 50;
        Item.height = 32;
        ////Tooltip.SetDefault("Their lives are yours\nShockblast + Transfusion bullets");
        Item.useTime = 8;
        Item.useAnimation = 8;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 350000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shootSpeed = 24f;
        Item.shoot = Mod.Find<ModProjectile>("ShockblastRound").Type;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("ShockblastRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SpectreBar, 5);
        recipe.AddIngredient(ItemID.PhoenixBlaster);
        recipe.AddIngredient(ItemID.ShroomiteBar, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}