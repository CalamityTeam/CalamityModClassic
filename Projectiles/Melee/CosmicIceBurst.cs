using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class CosmicIceBurst : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Ice");
			Main.projFrames[Projectile.type] = 9;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
			Projectile.timeLeft = 60;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 4;
        }

        public override void AI()
        {
			Projectile.ai[1] += 0.01f;
			Projectile.scale = Projectile.ai[1];
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= (float)(3 * Main.projFrames[Projectile.type])) 
			{
				Projectile.Kill();
				return;
			}
			if (++Projectile.frameCounter >= 3) 
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type]) 
				{
					Projectile.hide = true;
				}
			}
			Projectile.alpha -= 63;
			if (Projectile.alpha < 0) 
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight(Projectile.Center, 0.517f, ((float)Main.DiscoG / 300f), 0.85f);
			if (Projectile.ai[0] == 1f) 
			{
				Projectile.position = Projectile.Center;
				Projectile.width = (Projectile.height = (int)(52f * Projectile.scale));
				Projectile.Center = Projectile.position;
				Projectile.Damage();
				SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
				for (int num1000 = 0; num1000 < 4; num1000++) 
				{
					int num1001 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 187, 0f, 0f, 100, new Color(150, Main.DiscoG, 255), 1.5f);
					Main.dust[num1001].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
				}
				for (int num1002 = 0; num1002 < 10; num1002++) 
				{
					int num1003 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 200, new Color(150, Main.DiscoG, 255), 2.7f);
					Main.dust[num1003].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
					Main.dust[num1003].noGravity = true;
					Main.dust[num1003].velocity *= 3f;
					num1003 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 100, new Color(150, Main.DiscoG, 255), 1.5f);
					Main.dust[num1003].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)Projectile.width / 2f;
					Main.dust[num1003].velocity *= 2f;
					Main.dust[num1003].noGravity = true;
					Main.dust[num1003].fadeIn = 2.5f;
				}
				for (int num1004 = 0; num1004 < 5; num1004++) 
				{
					int num1005 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 0, new Color(150, Main.DiscoG, 255), 2.7f);
					Main.dust[num1005].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2)) * (float)Projectile.width / 2f;
					Main.dust[num1005].noGravity = true;
					Main.dust[num1005].velocity *= 3f;
				}
				for (int num1006 = 0; num1006 < 10; num1006++) 
				{
					int num1007 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 187, 0f, 0f, 0, new Color(150, Main.DiscoG, 255), 1.5f);
					Main.dust[num1007].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2)) * (float)Projectile.width / 2f;
					Main.dust[num1007].noGravity = true;
					Main.dust[num1007].velocity *= 3f;
				}
			}
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
        	Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
			if (Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type])
			{
				color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
			}
        	Vector2 vector42 = Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
			Texture2D texture2D33 = TextureAssets.Projectile[Projectile.type].Value;
			Microsoft.Xna.Framework.Rectangle rectangle15 = texture2D33.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
			Microsoft.Xna.Framework.Color alpha5 = Projectile.GetAlpha(color25);
			Vector2 origin11 = rectangle15.Size() / 2f;
			return true;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(150, Main.DiscoG, 255, 127);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(BuffID.Frostburn, 300);
        	Projectile.direction = Main.player[Projectile.owner].direction;
        }
    }
}