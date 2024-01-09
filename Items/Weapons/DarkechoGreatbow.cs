using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class DarkechoGreatbow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/DarkechoGreatbow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Darkecho Greatbow");
        Item.damage = 40;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 24;
        Item.height = 74;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        //AddTooltip("Chance to fire unholy arrows");
        //AddTooltip2("Fires two arrows at once");
        Item.knockBack = 4;
        Item.value = 175000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	for (int i = 0; i < 2; i++) // Will shoot 2 arrows
    	{
    		float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
        	float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
    		switch (Main.rand.Next(3))
			{
    			case 1: type = ProjectileID.UnholyArrow; break;
    			default: break;
			}
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Frostburn, 100);
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}