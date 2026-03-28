using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Patreon
{
    public class KeelhaulBubble : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bubble");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        
        public override void OnKill(int timeLeft)
        {
        	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y - 80f, 0f, 0f, Mod.Find<ModProjectile>("KeelhaulGeyserBottom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y - 80f, 0f, 0f, Mod.Find<ModProjectile>("KeelhaulGeyserTop").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}