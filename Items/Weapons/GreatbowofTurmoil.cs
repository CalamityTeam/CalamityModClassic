using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class GreatbowofTurmoil : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/GreatbowofTurmoil");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Greatbow of Turmoil");
        Item.damage = 49;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 34;
        Item.height = 46;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Chance to fire cursed, ichor, and hellfire arrows\nFires three arrows at once");
        Item.knockBack = 4f;
        Item.value = 300000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	for (int i = 0; i < 3; i++) // Will shoot 2 arrows
    	{
	    	float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
	       	float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
	    	switch (Main.rand.Next(6))
			{
	    		case 1: type = ProjectileID.CursedArrow; break;
	    		case 2: type = ProjectileID.HellfireArrow; break;
	    		case 3: type = ProjectileID.IchorArrow; break;
	    		default: break;
			}
	        Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, Main.myPlayer);
    	}
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
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}