using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class TerraEdgeBeam : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            AIType = 132;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 1.3f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
            if (Main.rand.NextBool(3))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GreenFairy, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GreenFairy, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
        	int healAmount = (Main.rand.Next(2) + 1);
            target.AddBuff(BuffID.Ichor, 60);
            Player player = Main.player[Projectile.owner];
			player.statLife += healAmount;
    		player.HealEffect(healAmount);
        }
    }
}