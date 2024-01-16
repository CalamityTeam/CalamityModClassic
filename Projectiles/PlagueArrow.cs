using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class PlagueArrow : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Plague Arrow");
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 600;
            AIType = 1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PlagueExplosionFriendly").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	if (Projectile.owner == Main.myPlayer)
			{
				int num516 = 6;
				for (int num517 = 0; num517 < num516; num517++)
				{
					if (num517 % 2 != 1 || Main.rand.Next(3) == 0)
					{
						Vector2 value20 = Projectile.position;
						Vector2 value21 = Projectile.oldVelocity;
						value21.Normalize();
						value21 *= 8f;
						float num518 = (float)Main.rand.Next(-35, 36) * 0.01f;
						float num519 = (float)Main.rand.Next(-35, 36) * 0.01f;
						value20 -= value21 * (float)num517;
						num518 += Projectile.oldVelocity.X / 6f;
						num519 += Projectile.oldVelocity.Y / 6f;
						int num520 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), value20.X, value20.Y, num518, num519, Main.player[Projectile.owner].beeType(), Main.player[Projectile.owner].beeDamage(Projectile.damage / 2), Main.player[Projectile.owner].beeKB(0f), Main.myPlayer, 0f, 0f);
						Main.projectile[num520].DamageType = DamageClass.Ranged;
						Main.projectile[num520].penetrate = 2;
					}
				}
			}
        }
    }
}