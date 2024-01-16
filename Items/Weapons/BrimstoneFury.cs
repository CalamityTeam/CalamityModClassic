using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BrimstoneFury : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/BrimstoneFury");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Brimstone Fury");
        Item.damage = 39;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 28;
        Item.height = 58;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Fires three brimstone bolts at once");
        Item.knockBack = 3.75f;
        Item.value = 300000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BrimstoneBolt").Type;
        Item.shootSpeed = 13f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	for (int i = 0; i < 3; i++) // Will shoot 5 arrows
    	{
            float SpeedX = velocity.X + 25 * 0.05f;
            float SpeedY = velocity.Y + 25 * 0.05f;
            float SpeedX2 = velocity.X - 25 * 0.05f;
            float SpeedY2 = velocity.Y - 25 * 0.05f;
            float SpeedX3 = velocity.X + 0 * 0.05f;
            float SpeedY3 = velocity.Y + 0 * 0.05f;
        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("BrimstoneBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, Mod.Find<ModProjectile>("BrimstoneBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX3, SpeedY3, Mod.Find<ModProjectile>("BrimstoneBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}

    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UnholyCore", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}