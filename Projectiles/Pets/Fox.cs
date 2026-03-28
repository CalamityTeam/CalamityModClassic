using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Pets
{
    public class Fox : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fox");
            Main.projFrames[Projectile.type] = 11;
            Main.projPet[Projectile.type] = true;
        }
    	
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.aiStyle = 26;
            AIType = 334;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (player.dead)
            {
                modPlayer.fox = false;
            }
            if (modPlayer.fox)
            {
                Projectile.timeLeft = 2;
            }
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}