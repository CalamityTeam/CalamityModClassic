using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SpatialSpear2 : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Spatial Spear");
            Projectile.width = 25;
            Projectile.height = 6;
            Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 120;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f);
            Projectile.velocity.Y += Projectile.ai[0];
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 60);
        	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 60);
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 60);
        	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 60);
			int numProj = 2;
            float rotation = MathHelper.ToRadians(20);
            for (int i = 0; i < numProj + 1; i++)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("SpatialSpear3").Type, (int)((double)Projectile.damage * 0.85f), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
            }
        }
    }
}