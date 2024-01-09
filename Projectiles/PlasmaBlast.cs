using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class PlasmaBlast : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Blast");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.alpha = 150;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            AIType = 503;
        }
        
        public override void AI()
		{
			if (Projectile.Center.Y > Projectile.ai[1])
			{
				Projectile.tileCollide = true;
			}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 20 + Main.rand.Next(40);
				SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
			}
			Projectile.alpha -= 15;
			int num58 = 150;
			if (Projectile.Center.Y >= Projectile.ai[1])
			{
				num58 = 0;
			}
			if (Projectile.alpha < num58)
			{
				Projectile.alpha = num58;
			}
			Projectile.localAI[0] += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * (float)Projectile.direction;
			Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
			if (Main.rand.NextBool(16))
			{
				Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
				int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				Main.dust[num59].velocity = value3 * 0.66f;
				Main.dust[num59].position = Projectile.Center + value3 * 12f;
			}
			if (Main.rand.NextBool(48))
			{
				int num60 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), 16, 1f);
				Main.gore[num60].velocity *= 0.66f;
				Main.gore[num60].velocity += Projectile.velocity * 0.3f;
			}
			if (Projectile.ai[1] == 1f)
			{
				Projectile.light = 0.9f;
				if (Main.rand.NextBool(10))
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				}
				if (Main.rand.NextBool(20))
				{
					Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18), 1f);
					return;
				}
			}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.7f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("PlasmaExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
    }
}