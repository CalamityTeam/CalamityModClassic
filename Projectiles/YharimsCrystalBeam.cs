﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;

namespace CalamityModClassic1Point2.Projectiles
{
    public class YharimsCrystalBeam : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Beam");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
        }
        
        public float GetHue(float indexing)
        {
        	string playerName;
        	if (Main.player[Projectile.owner].active && (playerName = Main.player[Projectile.owner].name) != null)
        	{
        		Dictionary<string, int> LaserHue = new Dictionary<string, int>(17)
        		{
        			{"Fabsol", 0},
        			{"Ziggums", 1},
        			{"Poly", 2},
        			{"Zach", 3},
        			{"Grox", 4},
        			{"Jenosis", 5},
        			{"DM DOKURO", 6},
        			{"Uncle Danny", 7},
        			{"Phoenix", 8},
        			{"Vlad", 9},
        			{"Khaelis", 10},
        			{"Purple Necromancer", 11},
        			{"Spoopyro", 12},
        			{"Svante", 13},
        			{"Puff", 14},
        			{"Echo", 15},
        			{"Testdude", 16}
        		};
        		int someNumber;
        		if (LaserHue.TryGetValue(playerName, out someNumber))
        		{
        			switch (someNumber)
					{
					case 0:
					case 1:
						return 2f;
					case 2:
						return 0.83f;
					case 3:
						return 1.5f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.1f;
					case 4:
						return 1.27f;
					case 5:
						return 0.65f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.1f;
					case 6:
						return 0f;
					case 7:
					case 8:
						return 1.7f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.07f;
					case 9:
						return 0.15f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.07f;
					case 10:
						return 1.15f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.18f;
					case 11:
						return 1.7f + (float)Math.Cos(Main.time / 120.0 * 6.2831854820251465) * 0.05f;
					case 12:
						return 0.83f + (float)Math.Cos(Main.time / 120.0 * 6.2831854820251465) * 0.03f;
					case 13:
						return 1.4f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.06f;
					case 14:
						return 0.31f + (float)Math.Cos(Main.time / 120.0 * 6.2831854820251465) * 0.13f;
					case 15:
						return 1.9f + (float)Math.Cos(Main.time / 180.0 * 6.2831854820251465) * 0.1f;
					case 16:
						return Main.rand.NextFloat();
					}
        		}
        	}
        	return (float)((int)indexing) / 6f;
        }

        public override void AI()
        {
			Vector2? vector71 = null;
			if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero) 
			{
				Projectile.velocity = -Vector2.UnitY;
			}
			if (Projectile.type != Mod.Find<ModProjectile>("YharimsCrystalBeam").Type || !Main.projectile[(int)Projectile.ai[1]].active || Main.projectile[(int)Projectile.ai[1]].type != Mod.Find<ModProjectile>("YharimsCrystal").Type)
			{
				Projectile.Kill();
				return;
			}
			float num810 = (float)((int)Projectile.ai[0]) - 2.5f;
			Vector2 value36 = Vector2.Normalize(Main.projectile[(int)Projectile.ai[1]].velocity);
			Projectile projectile2 = Main.projectile[(int)Projectile.ai[1]];
			float num811 = num810 * 0.5235988f;
			Vector2 value37 = Vector2.Zero;
			float num812;
			float y;
			float num813;
			float scaleFactor6;
			if (projectile2.ai[0] < 180f) 
			{
				num812 = 1f - projectile2.ai[0] / 180f;
				y = 20f - projectile2.ai[0] / 180f * 14f;
				if (projectile2.ai[0] < 120f) 
				{
					num813 = 20f - 4f * (projectile2.ai[0] / 120f);
					Projectile.Opacity = projectile2.ai[0] / 120f * 0.4f;
				} 
				else
				{
					num813 = 16f - 10f * ((projectile2.ai[0] - 120f) / 60f);
					Projectile.Opacity = 0.4f + (projectile2.ai[0] - 120f) / 60f * 0.6f;
				}
				scaleFactor6 = -22f + projectile2.ai[0] / 180f * 20f;
			} 
			else 
			{
				num812 = 0f;
				num813 = 1.75f;
				y = 6f;
				Projectile.Opacity = 1f;
				scaleFactor6 = -2f;
			}
			float num814 = (projectile2.ai[0] + num810 * num813) / (num813 * 6f) * 6.28318548f;
			num811 = Vector2.UnitY.RotatedBy((double)num814, default(Vector2)).Y * 0.5235988f * num812;
			value37 = (Vector2.UnitY.RotatedBy((double)num814, default(Vector2)) * new Vector2(4f, y)).RotatedBy((double)projectile2.velocity.ToRotation(), default(Vector2));
			Projectile.position = projectile2.Center + value36 * 16f - Projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)Projectile.ai[1]].gfxOffY);
			Projectile.position += projectile2.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
			Projectile.position += value37;
			Projectile.velocity = Vector2.Normalize(projectile2.velocity).RotatedBy((double)num811, default(Vector2));
			Projectile.scale = 1.8f * (1f - num812);
			Projectile.damage = projectile2.damage;
			if (projectile2.ai[0] >= 180f) 
			{
				Projectile.damage *= 10;
				vector71 = new Vector2?(projectile2.Center);
			}
			if (!Collision.CanHitLine(Main.player[Projectile.owner].Center, 0, 0, projectile2.Center, 0, 0)) 
			{
				vector71 = new Vector2?(Main.player[Projectile.owner].Center);
			}
			Projectile.friendly = (projectile2.ai[0] > 30f);
			if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero) 
			{
				Projectile.velocity = -Vector2.UnitY;
			}
			float num818 = Projectile.velocity.ToRotation();
			Projectile.rotation = num818 - 1.57079637f;
			Projectile.velocity = num818.ToRotationVector2();
			float num819 = 2f;
			float num820 = 0f;
			Vector2 samplingPoint = Projectile.Center;
			if (vector71.HasValue) 
			{
				samplingPoint = vector71.Value;
			}
			float[] array3 = new float[(int)num819];
			Collision.LaserScan(samplingPoint, Projectile.velocity, num820 * Projectile.scale, 2400f, array3);
			float num821 = 0f;
			for (int num822 = 0; num822 < array3.Length; num822++) 
			{
				num821 += array3[num822];
			}
			num821 /= num819;
			float amount = 0.75f;
			Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num821, amount);
			if (Math.Abs(Projectile.localAI[1] - num821) < 100f && Projectile.scale > 0.15f)
			{
				float prismHue = GetHue(Projectile.ai[0]);
				Color color = Main.hslToRgb(2.55f, prismHue, 0.53f);
				color.A = 0;
				Vector2 vector80 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14.5f * Projectile.scale);
				float x = Main.rgbToHsl(new Color(255, Main.DiscoG, 53)).X;
				for (int num843 = 0; num843 < 2; num843++) 
				{
					float num844 = Projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)) ? -1f : 1f) * 1.57079637f;
					float num845 = (float)Main.rand.NextDouble() * 0.8f + 1f;
					Vector2 vector81 = new Vector2((float)Math.Cos((double)num844) * num845, (float)Math.Sin((double)num844) * num845);
					int num846 = Dust.NewDust(vector80, 0, 0, DustID.CopperCoin, vector81.X, vector81.Y, 0, new Color(255, Main.DiscoG, 53), 3.3f); //267
					Main.dust[num846].color = color;
					Main.dust[num846].scale = 1.2f;
					if (Projectile.scale > 1f) 
					{
						Main.dust[num846].velocity *= Projectile.scale;
						Main.dust[num846].scale *= Projectile.scale;
					}
					Main.dust[num846].noGravity = true;
					if (Projectile.scale != 1.4f) 
					{
						Dust dust9 = Dust.CloneDust(num846);
						dust9.color = Color.Orange;
						dust9.scale /= 2f;
					}
					float hue = (x + Main.rand.NextFloat() * 0.4f) % 1f;
					Main.dust[num846].color = Color.Lerp(color, Main.hslToRgb(2.55f, hue, 0.53f), Projectile.scale / 1.4f);
				}
				if (Main.rand.NextBool(5)) 
				{
					Vector2 value42 = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)Projectile.width;
					int num847 = Dust.NewDust(vector80 + value42 - Vector2.One * 4f, 8, 8, DustID.CopperCoin, 0f, 0f, 100, new Color(255, Main.DiscoG, 53), 5f);
					Main.dust[num847].velocity *= 0.5f;
					Main.dust[num847].velocity.Y = -Math.Abs(Main.dust[num847].velocity.Y);
				}
				DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
				float value43 = 0.1f * (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 20f));
				Vector2 size = new Vector2(Projectile.velocity.Length() * Projectile.localAI[1], (float)Projectile.width * Projectile.scale);
				float num848 = Projectile.velocity.ToRotation();
				if (Main.netMode != NetmodeID.Server) 
				{
					((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(Projectile.position + new Vector2(size.X * 0.5f, 0f).RotatedBy((double)num848, default(Vector2)), new Color(0.5f, 0.1f * (float)Math.Sign(value43) + 0.5f, 0f, 1f) * Math.Abs(value43), size, RippleShape.Square, num848);
				}
				Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, (DelegateMethods.CastLight));
				return;
			}
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	if (Projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			float num228 = Projectile.localAI[1];
			float prismHue = GetHue(Projectile.ai[0]);
			Microsoft.Xna.Framework.Color value25 = Main.hslToRgb(2.55f, prismHue, 0.53f);
			value25.A = 0;
			Vector2 value26 = Projectile.Center.Floor();
			value26 += Projectile.velocity * Projectile.scale * 10.5f;
			num228 -= Projectile.scale * 14.5f * Projectile.scale;
			Vector2 vector29 = new Vector2(Projectile.scale);
			DelegateMethods.f_1 = 1f;
			DelegateMethods.c_1 = value25 * 0.75f * Projectile.Opacity;
			Vector2 projPos = Projectile.oldPos[0];
			projPos = new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
			Utils.DrawLaser(Main.spriteBatch, tex, value26 - Main.screenPosition, value26 + Projectile.velocity * num228 - Main.screenPosition, vector29, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			DelegateMethods.c_1 = new Microsoft.Xna.Framework.Color(255, Main.DiscoG, 53, 127) * 0.75f * Projectile.Opacity;
			Utils.DrawLaser(Main.spriteBatch, tex, value26 - Main.screenPosition, value26 + Projectile.velocity * num228 - Main.screenPosition, vector29 / 2f, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			return false;
        }
        
        public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, (DelegateMethods.CutTiles));
		}
        
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
        	if (projHitbox.Intersects(targetHitbox))
			{
				return true;
			}
        	float num6 = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], 22f * Projectile.scale, ref num6))
			{
				return true;
			}
			return false;
        }
    }
}