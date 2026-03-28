using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class ScarletDevilBullet : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gungnir Bullet");
		}
		
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
			Projectile.timeLeft = 140;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
        	if (Projectile.ai[0] <= 60f)
        	{
				Projectile.velocity.X *= 0.975f;
				Projectile.velocity.Y *= 0.975f;
			}
			else
			{
				float centerX = Projectile.Center.X;
				float centerY = Projectile.Center.Y;
				float num474 = 1000f;
				bool homeIn = false;
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].CanBeChasedBy(Projectile, false))
					{
						float num476 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
						float num477 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
						float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
						if (num478 < num474)
						{
							num474 = num478;
							centerX = num476;
							centerY = num477;
							homeIn = true;
						}
					}
				}
				if (homeIn)
				{
					float num483 = 30f;
					Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num484 = centerX - vector35.X;
					float num485 = centerY - vector35.Y;
					float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
					num486 = num483 / num486;
					num484 *= num486;
					num485 *= num486;
					Projectile.velocity.X = (Projectile.velocity.X * 10f + num484) / 11f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 10f + num485) / 11f;
					return;
				}
				else
				{
					Projectile.velocity.X = 0f;
					Projectile.velocity.Y = 0f;
				}
			}
        }
		
		public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 250, 250);
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			CalamityPlayerPreTrailer modPlayer = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>();
        	if (target.type == NPCID.TargetDummy || modPlayer.rogueStealth <= 0f)
			{
				return;
			}
        	Main.player[Main.myPlayer].statLife += 1;
			Main.player[Main.myPlayer].HealEffect(1);
        }
    }
}
