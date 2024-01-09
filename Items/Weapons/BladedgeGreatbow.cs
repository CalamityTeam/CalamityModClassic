using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class BladedgeGreatbow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/BladedgeGreatbow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Bladedge Greatbow");
        Item.damage = 35;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 46;
        Item.height = 88;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        //AddTooltip("Chance to fire chlorophyte arrows");
        //AddTooltip2("Fires five arrows at once");
        Item.knockBack = 3.5f;
        Item.value = 200000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	for (int i = 0; i < 5; i++) // Will shoot 5 arrows
    	{
            float SpeedX = velocity.X + (float) Main.rand.Next(-60, 61) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-60, 61) * 0.05f;
    		switch (Main.rand.Next(4))
			{
    			case 1: type = ProjectileID.ChlorophyteArrow; break;
    			default: break;
			}
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	}
    	return false;
	}
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Venom, 200);
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 12);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}