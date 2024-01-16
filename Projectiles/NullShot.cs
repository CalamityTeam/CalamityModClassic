﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class NullShot : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Null Shot");
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            for (int num134 = 0; num134 < 10; num134++)
			{
				float x = Projectile.position.X - Projectile.velocity.X / 10f * (float)num134;
				float y = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num134;
				int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 160, 0f, 0f, 0, default(Color), 2f);
				Main.dust[num135].alpha = Projectile.alpha;
				Main.dust[num135].position.X = x;
				Main.dust[num135].position.Y = y;
				Main.dust[num135].velocity *= 0f;
				Main.dust[num135].noGravity = true;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	int nullBuff = Main.rand.Next(7);
        	if (!target.boss)
        	{
        		if (nullBuff == 0)
        		{
        			target.scale *= 2f;
        		}
        		else if (nullBuff == 1)
        		{
        			target.scale *= 0.5f;
        		}
        		else if (nullBuff == 2)
        		{
        			target.damage += 100;
        		}
        		else if (nullBuff == 3)
        		{
        			target.damage -= 100;
        		}
        		else if (nullBuff == 4)
        		{
        			target.knockBackResist = 0f;
        		}
        		else if (nullBuff == 5)
        		{
        			target.knockBackResist = 1f;
        		}
        		else if (nullBuff == 6)
        		{
        			target.defense += 10;
        		}
        		else
        		{
        			target.defense -= 10;
        		}
        	}
        }
    }
}