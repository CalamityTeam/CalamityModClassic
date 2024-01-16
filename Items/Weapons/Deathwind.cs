using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Deathwind : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/10.DevourerofGods/Deathwind");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Deathwind");
        Item.damage = 175;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 18;
        Item.height = 62;
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Fires lasers of death");
        Item.knockBack = 5;
        Item.value = 1250000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("NebulaShot").Type;
		Item.shootSpeed = 20f;
		Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	float SpeedA = velocity.X;
   		float SpeedB = velocity.Y;
        int num6 = Main.rand.Next(4, 8);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
    		if (Main.rand.Next(3) == 0)
	        {
	        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("IceBeam").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	        else
	        {
	        	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), Mod.Find<ModProjectile>("NebulaShot").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
    	}
    	return false;
	}
}}