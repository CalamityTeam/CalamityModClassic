using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class Celestus2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Celestus");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 4;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            Projectile.timeLeft = 85;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
        
        public override void AI()
        {
            Projectile.rotation += 2f;
            Projectile.velocity.X *= 1.02f;
            Projectile.velocity.Y *= 1.02f;
        	Lighting.AddLight(Projectile.Center, ((Main.DiscoR - Projectile.alpha) * 0.5f) / 255f, ((Main.DiscoG - Projectile.alpha) * 0.5f) / 255f, ((Main.DiscoB - Projectile.alpha) * 0.5f) / 255f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.timeLeft < 85)
            {
                byte b2 = (byte)(Projectile.timeLeft * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            return new Color(255, 255, 255, 100);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(30) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 240);
            }
            target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 100);
            target.AddBuff(BuffID.CursedInferno, 100);
            target.AddBuff(BuffID.Frostburn, 100);
            target.AddBuff(BuffID.OnFire, 100);
            target.AddBuff(BuffID.Ichor, 100);
        }
    }
}