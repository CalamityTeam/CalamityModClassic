using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class AMR : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AMR");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.light = 0.5f;
            Projectile.alpha = 255;
			Projectile.extraUpdates = 10;
			Projectile.scale = 1.18f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 1;
            AIType = 242;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                for (int x = 0; x < 8; x++)
                {
                    float xPos = Projectile.ai[0] > 0 ? Projectile.position.X + 500 : Projectile.position.X - 500;
                    Vector2 vector2 = new Vector2(xPos, Projectile.position.Y + Main.rand.Next(-500, 501));
                    float num80 = xPos;
                    float speedX = (float)target.position.X - vector2.X;
                    float speedY = (float)target.position.Y - vector2.Y;
                    float dir = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                    dir = 10 / num80;
                    speedX *= dir * 150;
                    speedY *= dir * 150;
                    if (Projectile.owner == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX, speedY, Mod.Find<ModProjectile>("AMR2").Type, (int)((double)Projectile.damage * 0.1f), 1f, Projectile.owner);
                    }
                }
            }
            target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 600);
            if (target.defense > 50)
            {
                target.defense -= 50;
            }
        }
    }
}