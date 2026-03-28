using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class Triploon : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Triploon");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
        	if (Main.player[Projectile.owner].dead)
			{
				Projectile.Kill();
				return;
			}
        	if (Projectile.alpha == 0)
			{
				if (Projectile.position.X + (float)(Projectile.width / 2) > Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2))
				{
					Main.player[Projectile.owner].ChangeDir(1);
				}
				else
				{
					Main.player[Projectile.owner].ChangeDir(-1);
				}
			}
			if (Projectile.ai[0] == 0f)
			{
				Projectile.extraUpdates = 0;
			}
			else
			{
				Projectile.extraUpdates = 1;
			}
			Vector2 vector14 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num166 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector14.X;
			float num167 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector14.Y;
			float num168 = (float)Math.Sqrt((double)(num166 * num166 + num167 * num167));
			if (Projectile.ai[0] == 0f)
			{
				if (num168 > 700f)
				{
					Projectile.ai[0] = 1f;
				}
				else if (num168 > 350f)
				{
					Projectile.ai[0] = 1f;
				}
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
				Projectile.ai[1] += 1f;
				if (Projectile.ai[1] > 5f)
				{
					Projectile.alpha = 0;
				}
				if (Projectile.ai[1] > 8f)
				{
					Projectile.ai[1] = 8f;
				}
				if (Projectile.ai[1] >= 10f)
				{
					Projectile.ai[1] = 15f;
					Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
				}
			}
			else if (Projectile.ai[0] == 1f)
			{
				Projectile.tileCollide = false;
				Projectile.rotation = (float)Math.Atan2((double)num167, (double)num166) - 1.57f;
				float num169 = 20f;
				if (num168 < 50f)
				{
					Projectile.Kill();
				}
				num168 = num169 / num168;
				num166 *= num168;
				num167 *= num168;
				Projectile.velocity.X = num166;
				Projectile.velocity.Y = num167;
			}
        }
    }
}