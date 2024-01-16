using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class PlagueStingerGoliath : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Plague Stinger");
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            AIType = 55;
            Projectile.scale = 1.25f;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 89, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240);
        }
    }
}