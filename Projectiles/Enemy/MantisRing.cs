using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityModClassicPreTrailer.Projectiles.Enemy
{
    public class MantisRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.knockBack = 3f;
            Projectile.width = 72;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.penetrate = 8;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);

            for (int i = 0; i < 60; i++)
            {
                float angle = MathHelper.TwoPi * Main.rand.NextFloat(0f, 1f);
                Vector2 angleVec = angle.ToRotationVector2();
                float distance = Main.rand.NextFloat(14f, 36f);
                Vector2 off = angleVec * distance;
                off.Y *= ((float)Projectile.height / Projectile.width);
                Vector2 pos = Projectile.Center + off;
                Dust d = Dust.NewDustPerfect(pos, ModContent.DustType<AstralBlue>(), angleVec * Main.rand.NextFloat(2f, 4f));
                d.customData = true;
            }
        }

        public override void AI()
        {
            Projectile.velocity *= 1.01f;

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                {
                    Projectile.frame = 0;
                }
            }

            //Dust
            for (int i = 0; i < 4; i++)
            {
                float angle = MathHelper.TwoPi * Main.rand.NextFloat(0f, 1f);
                float distance = Main.rand.NextFloat(14f, 36f);
                Vector2 off = angle.ToRotationVector2() * distance;
                off.Y *= ((float)Projectile.height / Projectile.width);
                Vector2 pos = Projectile.Center + off;
                Dust.NewDustPerfect(pos, ModContent.DustType<AstralBlue>(), Vector2.Zero);
            }
        }
    }
}
