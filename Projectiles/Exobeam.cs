using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class Exobeam : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
        }

        public override void AI()
        {
        	if (Projectile.localAI[1] == 0f)
			{
				SoundEngine.PlaySound(SoundID.Item60, Projectile.position);
				Projectile.localAI[1] += 1f;
			}
			Projectile.alpha -= 40;
			if (Projectile.alpha < 0) 
			{
				Projectile.alpha = 0;
			}
			if (Projectile.ai[0] == 0f) 
			{
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] >= 90f) 
				{
					Projectile.localAI[0] = 0f;
					Projectile.ai[0] = 1f;
					Projectile.netUpdate = true;
				}
				Projectile.velocity.Y = Projectile.velocity.Y - 0.01f;
				if (Projectile.velocity.Y > 0f) 
				{
					Projectile.velocity.Y = Projectile.velocity.Y - 0.02f;
				}
				if (Projectile.velocity.Y < -12f) 
				{
					Projectile.velocity.Y = -12f;
				}
			} 
			else if (Projectile.ai[0] == 1f) 
			{
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] >= 60f)
				{
					Projectile.localAI[0] = 0f;
					Projectile.ai[0] = 2f;
					Projectile.ai[1] = (float)Player.FindClosest(Projectile.position, Projectile.width, Projectile.height);
					Projectile.netUpdate = true;
				}
				Projectile.velocity.Y = Projectile.velocity.Y - 0.01f;
				if (Projectile.velocity.Y > 0f) 
				{
					Projectile.velocity.Y = Projectile.velocity.Y - 0.02f;
				}
				if (Projectile.velocity.Y < -12f) 
				{
					Projectile.velocity.Y = -12f;
				}
			} 
			else if (Projectile.ai[0] == 2f) 
			{
				Vector2 vector70 = Main.player[(int)Projectile.ai[1]].Center - Projectile.Center;
				if (vector70.Length() < 30f) 
				{
					Projectile.Kill();
					return;
				}
				vector70.Normalize();
				vector70 *= 14f;
				vector70 = Vector2.Lerp(Projectile.velocity, vector70, 0.6f);
				if (vector70.Y < 24f) 
				{
					vector70.Y = 24f;
				}
				float num804 = 0.4f;
				if (Projectile.velocity.X < vector70.X) 
				{
					Projectile.velocity.X = Projectile.velocity.X + num804;
					if (Projectile.velocity.X < 0f && vector70.X > 0f) 
					{
						Projectile.velocity.X = Projectile.velocity.X + num804;
					}
				} 
				else if (Projectile.velocity.X > vector70.X) 
				{
					Projectile.velocity.X = Projectile.velocity.X - num804;
					if (Projectile.velocity.X > 0f && vector70.X < 0f) 
					{
						Projectile.velocity.X = Projectile.velocity.X - num804;
					}
				}
				if (Projectile.velocity.Y < vector70.Y) 
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num804;
					if (Projectile.velocity.Y < 0f && vector70.Y > 0f) 
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num804;
					}
				} 
				else if (Projectile.velocity.Y > vector70.Y) 
				{
					Projectile.velocity.Y = Projectile.velocity.Y - num804;
					if (Projectile.velocity.Y > 0f && vector70.Y < 0f) 
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num804;
					}
				}
			}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
			return;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (Main.rand.NextBool(30))
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
			if (target.type == NPCID.TargetDummy)
			{
				return;
			}
			float num = (float)hit.Damage * 0.01f;
			if ((int)num == 0)
			{
				return;
			}
			if (Main.player[Main.myPlayer].lifeSteal <= 0f)
			{
				return;
			}
			Main.player[Main.myPlayer].lifeSteal -= num;
			int num2 = Projectile.owner;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X, target.position.Y, 0f, 0f, Mod.Find<ModProjectile>("Exoheal").Type, 0, 0f, Projectile.owner, (float)num2, num);
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(0, 255, 255, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.position);
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 200);
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num193 = 0; num193 < 6; num193++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			}
			for (int num194 = 0; num194 < 60; num194++)
			{
				int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
				num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
				Main.dust[num195].velocity *= 2f;
				Main.dust[num195].noGravity = true;
			}
			Projectile.Damage();
        }
    }
}