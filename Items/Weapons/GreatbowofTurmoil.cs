using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class GreatbowofTurmoil : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/GreatbowofTurmoil");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Greatbow of Turmoil");
        Item.damage = 42;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 34;
        Item.height = 46;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        //AddTooltip("Chance to fire cursed, ichor, and hellfire arrows");
        Item.knockBack = 4f;
        Item.value = 300000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(6))
		{
    		case 1: type = ProjectileID.CursedArrow; break;
    		case 2: type = ProjectileID.HellfireArrow; break;
    		case 3: type = ProjectileID.IchorArrow; break;
    		default: break;
		}
        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
    	return false;
	}
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 300);
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 10);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}