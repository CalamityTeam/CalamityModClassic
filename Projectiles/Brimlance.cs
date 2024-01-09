using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Brimlance : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimlance");
		}
    	
        public override void SetDefaults()
        {
			Projectile.width = 40;  //The width of the .png file in pixels divided by 2.
			Projectile.aiStyle = 19;
			Projectile.DamageType = DamageClass.Melee;  //Dictates whether this is a melee-class weapon.
			Projectile.timeLeft = 90;
			Projectile.height = 40;  //The height of the .png file in pixels divided by 2.
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
        	if (Main.rand.NextBool(4))
            {
        		int num = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.LifeDrain, (float)(Projectile.direction * 2), 0f, 150, default(Color), 1f);
            	Main.dust[num].noGravity = true;
            }
        	if(Projectile.ai[0] == 0f)
        	{
        		Projectile.ai[0] = 3f;
        		Projectile.netUpdate = true;
        	}
        	if(Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
        	{
        		Projectile.ai[0] -= 2.4f;
				if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
				{
					Projectile.localAI[0] = 1f;
				}
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
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    	{
        	target.immune[Projectile.owner] = 7;
        	if (target.life <= 0)
        	{
        		int num251 = Main.rand.Next(2, 5);
        		if (Projectile.owner == Main.myPlayer)
        		{
        			int fire = Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HellfireExplosionFriendly").Type, (int)((double)Projectile.damage * 0.75), hit.Knockback, Main.myPlayer);
		        	Main.projectile[fire].timeLeft = 60;
		        	Main.projectile[fire].DamageType = DamageClass.Melee;
					for (int num252 = 0; num252 < num251; num252++)
					{
						Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						while (value15.X == 0f && value15.Y == 0f)
						{
							value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						}
						value15.Normalize();
						value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
						int standFire = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.oldPosition.X + (float)(Projectile.width / 2), Projectile.oldPosition.Y + (float)(Projectile.height / 2), value15.X, value15.Y, Mod.Find<ModProjectile>("StandingFire").Type, (int)((double)Projectile.damage * 0.5), 0f, Projectile.owner, 0f, 0f);
						Main.projectile[standFire].DamageType = DamageClass.Melee;
					}
        		}
        	}
		}
    }
}