using System;
using System.Collections.Generic;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Astral
{
    public class Hiveling : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hiveling");
			Main.projFrames[Projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.minion = true;
			Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        
        public override void AI()
        {
			Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	Projectile.spriteDirection = -Projectile.direction;
            Projectile.rotation = Projectile.velocity.X * 0.05f;
        	float centerX = Projectile.Center.X;
			float centerY = Projectile.Center.Y;
			float num474 = 1200f;
			bool homeIn = false;
			int target = (int)Projectile.ai[0];
			if (Main.npc[target].CanBeChasedBy(Projectile, false))
			{
				float num476 = Main.npc[target].position.X + (float)(Main.npc[target].width / 2);
				float num477 = Main.npc[target].position.Y + (float)(Main.npc[target].height / 2);
				float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
				if (num478 < num474)
				{
					num474 = num478;
					centerX = num476;
					centerY = num477;
					homeIn = true;
				}
			}
			if (homeIn)
			{
				float num483 = 10f;
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num484 = centerX - vector35.X;
				float num485 = centerY - vector35.Y;
				float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 30f + num484) / 31f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 30f + num485) / 31f;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
			for (int k = 0; k < 3; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AstralOrange>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}