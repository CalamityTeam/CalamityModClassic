using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class LifeScythe : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Scythe");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 70;
            Projectile.aiStyle = 18;
            Projectile.alpha = 55;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
            AIType = 274;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 4;
        }
        
        public override void AI()
        {
        	if (Projectile.localAI[0] == 0f)
			{
				Projectile.scale -= 0.02f;
				Projectile.alpha += 30;
				if (Projectile.alpha >= 250)
				{
					Projectile.alpha = 255;
					Projectile.localAI[0] = 1f;
				}
			}
			else if (Projectile.localAI[0] == 1f)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 30;
				if (Projectile.alpha <= 0)
				{
					Projectile.alpha = 0;
					Projectile.localAI[0] = 0f;
				}
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f);
        	if (Main.rand.NextBool(3))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.CursedTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (target.type == NPCID.TargetDummy)
			{
				return;
			}
        	Player player = Main.player[Projectile.owner];
			player.statLife += 2;
    		player.HealEffect(2);
            target.AddBuff(BuffID.OnFire, 200);
			target.AddBuff(BuffID.CursedInferno, 200);
        }
    }
}