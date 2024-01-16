using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class NuclearFuryProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Nuclear Fury Projectile");
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.aiStyle = 71;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            AIType = 409;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 34, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(0, 0, 200, 0);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
		}
        
        public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width, Projectile.height);
            int frameHeight = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int frameY = frameHeight * Projectile.frame;
            Rectangle rectangle = new Rectangle(0, frameY, TextureAssets.Projectile[Projectile.type].Value.Width, frameHeight);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle?(rectangle), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
		}
    }
}