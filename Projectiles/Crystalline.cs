using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class Crystalline : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Crystalline");
            Projectile.width = 38;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.scale = 0.75f;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 113;
            Projectile.timeLeft = 120;
            AIType = 598;
        }
        
        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        	Projectile.ai[1] += 1f;
        	if (Projectile.ai[1] == 30f)
        	{
        		int numProj = 2;
	            float rotation = MathHelper.ToRadians(50);
	            for (int i = 0; i < numProj + 1; i++)
	            {
	                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
	                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("Crystalline2").Type, (int)((double)Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner, 0f, 0f);
	            }
        	}
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 154, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}