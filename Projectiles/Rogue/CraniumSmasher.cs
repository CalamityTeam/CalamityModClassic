using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class CraniumSmasher : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cranium Smasher");
		}
    	
         public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 300;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 15;
			Projectile.tileCollide = false;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
		
		public override void AI()
        {
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 3f)
			{
				Projectile.tileCollide = true;
			}
			Projectile.rotation += Projectile.velocity.X * 0.02f;
			Projectile.velocity.Y = Projectile.velocity.Y + 0.085f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
        }
        
        public override void OnKill(int timeLeft)
        {
        	for (int i = 0; i < 5; i++)
            {
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 5, (Projectile.oldVelocity.X / 2), (Projectile.oldVelocity.Y / 2), 0, default(Color), 2f);
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, (Projectile.oldVelocity.X / 2), (Projectile.oldVelocity.Y / 2), 0, default(Color), 1f);
			}
        }
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.penetrate == 0)
			{
				Projectile.Kill();
			}
			return false;
		}
    }
}