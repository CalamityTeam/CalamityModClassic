﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class MarniteSpearProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Spear");
		}
    	
        public override void SetDefaults()
        {
			Projectile.width = 50;  //The width of the .png file in pixels divided by 2.
			Projectile.aiStyle = 19;
			Projectile.DamageType = DamageClass.Melee;  //Dictates whether this is a melee-class weapon.
			Projectile.timeLeft = 90;
			Projectile.height = 50;  //The height of the .png file in pixels divided by 2.
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true;
        }

        public override void AI()
        {
        	Main.player[Projectile.owner].direction = Projectile.direction;
        	Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
        	Main.player[Projectile.owner].itemTime = Main.player[Projectile.owner].itemAnimation;
        	Projectile.position.X = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - (float)(Projectile.width / 2);
        	Projectile.position.Y = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - (float)(Projectile.height / 2);
        	Projectile.position += Projectile.velocity * Projectile.ai[0];
        	if(Projectile.ai[0] == 0f)
        	{
        		Projectile.ai[0] = 3f;
        		Projectile.netUpdate = true;
        	}
        	if(Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
        	{
        		Projectile.ai[0] -= 1.1f;
        	}
        	else
        	{
        		Projectile.ai[0] += 0.95f;
        	}
        	
        	if(Main.player[Projectile.owner].itemAnimation == 0)
        	{
        		Projectile.Kill();
        	}
        	
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        }
    }
}