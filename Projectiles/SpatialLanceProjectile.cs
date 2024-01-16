using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class SpatialLanceProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lance");
		}
    	
        public override void SetDefaults()
        {
			Projectile.width = 40;  //The width of the .png file in pixels divided by 2.
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
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 3;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Adjust owner stats based on this projectile
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;

            // Stick to the player
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter);

            // And move outward/inward based on the speed variable.
            Projectile.position += Projectile.velocity * Projectile.ai[0];
            if (Main.rand.NextBool(5))
            {
            	int num = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.RainbowTorch, (float)(Projectile.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
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
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + Projectile.velocity.X * Projectile.ai[0], Projectile.Center.Y + Projectile.velocity.Y * Projectile.ai[0], Projectile.velocity.X * 1.4f, Projectile.velocity.Y * 1.4f, Mod.Find<ModProjectile>("SpatialSpear").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
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
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 300);
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300);
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Vector2 origin = Vector2.Zero;
            Main.EntitySpriteDraw(texture, drawPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, 0, 0);
            return false;
        }
    }
}