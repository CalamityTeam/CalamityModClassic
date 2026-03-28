using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class Vortex : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vortex");
			Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
        }
        
        public override void AI()
        {
			Projectile.localAI[1] += 1f;
			if (Projectile.localAI[1] > 10f && Main.rand.Next(3) == 0) 
			{
				int num713 = 5;
				for (int num714 = 0; num714 < num713; num714++) 
				{
					Vector2 vector58 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width, (float)Projectile.height) / 2f;
					vector58 = vector58.RotatedBy((double)(num714 - (num713 / 2 - 1)) * 3.1415926535897931 / (double)((float)num713), default(Vector2)) + Projectile.Center;
					Vector2 value25 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int num715 = Dust.NewDust(vector58 + value25, 0, 0, 66, value25.X * 2f, value25.Y * 2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.7f);
					Main.dust[num715].noGravity = true;
					Main.dust[num715].noLight = true;
					Main.dust[num715].velocity /= 4f;
					Main.dust[num715].velocity -= Projectile.velocity;
				}
				Projectile.alpha -= 5;
				if (Projectile.alpha < 50) 
				{
					Projectile.alpha = 50;
				}
				Projectile.rotation += Projectile.velocity.X * 0.1f;
				Projectile.frame = (int)(Projectile.localAI[1] / 3f) % 3;
				Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, ((float)Main.DiscoR / 200f), ((float)Main.DiscoG / 200f), ((float)Main.DiscoB / 200f));
			}
			int num716 = -1;
			Vector2 vector59 = Projectile.Center;
			float num717 = 350f * Projectile.ai[1];
			if (Projectile.localAI[0] > 0f) 
			{
				Projectile.localAI[0] -= 1f;
			}
			if (Projectile.ai[0] == 0f && Projectile.localAI[0] == 0f) 
			{
				for (int num718 = 0; num718 < 200; num718++) 
				{
					NPC nPC6 = Main.npc[num718];
					if (nPC6.CanBeChasedBy(Projectile, false) && (Projectile.ai[0] == 0f || Projectile.ai[0] == (float)(num718 + 1))) 
					{
						Vector2 center4 = nPC6.Center;
						float num719 = Vector2.Distance(center4, vector59);
						if (num719 < num717 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC6.position, nPC6.width, nPC6.height)) 
						{
							num717 = num719;
							vector59 = center4;
							num716 = num718;
						}
					}
				}
				if (num716 >= 0) 
				{
					Projectile.ai[0] = (float)(num716 + 1);
					Projectile.netUpdate = true;
				}
			}
			if (Projectile.localAI[0] == 0f && Projectile.ai[0] == 0f) 
			{
				Projectile.localAI[0] = 30f;
			}
			bool flag32 = false;
			if (Projectile.ai[0] != 0f) 
			{
				int num720 = (int)(Projectile.ai[0] - 1f);
				if (Main.npc[num720].active && !Main.npc[num720].dontTakeDamage && Main.npc[num720].immune[Projectile.owner] == 0) 
				{
					float num721 = Main.npc[num720].position.X + (float)(Main.npc[num720].width / 2);
					float num722 = Main.npc[num720].position.Y + (float)(Main.npc[num720].height / 2);
					float num723 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num721) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num722);
					if (num723 < 1000f) 
					{
						flag32 = true;
						vector59 = Main.npc[num720].Center;
					}
				} 
				else 
				{
					Projectile.ai[0] = 0f;
					flag32 = false;
					Projectile.netUpdate = true;
				}
			}
			if (flag32) 
			{
				Vector2 v = vector59 - Projectile.Center;
				float num724 = Projectile.velocity.ToRotation();
				float num725 = v.ToRotation();
				double num726 = (double)(num725 - num724);
				if (num726 > 3.1415926535897931) 
				{
					num726 -= 6.2831853071795862;
				}
				if (num726 < -3.1415926535897931) 
				{
					num726 += 6.2831853071795862;
				}
				Projectile.velocity = Projectile.velocity.RotatedBy(num726 * 0.10000000149011612, default(Vector2));
			}
			float num727 = Projectile.velocity.Length();
			Projectile.velocity.Normalize();
			Projectile.velocity *= num727 + 0.0025f;
			return;
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, Projectile.alpha);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (Main.rand.Next(75) == 0)
	    	{
	    		target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 120);
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
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}