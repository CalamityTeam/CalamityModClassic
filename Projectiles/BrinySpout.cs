﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BrinySpout : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Briny Spout");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 60;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10 -
				(NPC.downedFishron ? 3 : 0) -
        		(NPC.downedMoonlord ? 3 : 0) -
        		(CalamityWorld1Point2.downedDoG ? 2 : 0) -
        		(CalamityWorld1Point2.downedYharon ? 1 : 0);
        }
        
        public override void AI()
        {
			bool dukeFish = NPC.downedFishron;
			int num613 = dukeFish ? 16 : 10;
			int num614 = dukeFish ? 16 : 15;
			float num615 = dukeFish ? 1.5f : 1f;
			int num616 = 150;
			int num617 = 42;
			if (Main.rand.NextBool(15))
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Flare_Blue, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 100, new Color(53, Main.DiscoG, 255));
            }
			if (Projectile.velocity.X != 0f)
			{
				Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.scale = ((float)(num613 + num614) - Projectile.ai[1]) * num615 / (float)(num614 + num613);
				Projectile.width = (int)((float)num616 * Projectile.scale);
				Projectile.height = (int)((float)num617 * Projectile.scale);
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[1] != -1f)
			{
				Projectile.scale = ((float)(num613 + num614) - Projectile.ai[1]) * num615 / (float)(num614 + num613);
				Projectile.width = (int)((float)num616 * Projectile.scale);
				Projectile.height = (int)((float)num617 * Projectile.scale);
			}
			if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.alpha -= 3;
				if (Projectile.alpha < 60)
				{
					Projectile.alpha = 60;
				}
			}
			else
			{
				Projectile.alpha += 3;
				if (Projectile.alpha > 150)
				{
					Projectile.alpha = 150;
				}
			}
			if (Projectile.ai[0] > 0f)
			{
				Projectile.ai[0] -= 1f;
			}
			if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
			{
				Projectile.netUpdate = true;
				Vector2 center = Projectile.Center;
				center.Y -= (float)num617 * Projectile.scale / 2f;
				float num618 = ((float)(num613 + num614) - Projectile.ai[1] + 1f) * num615 / (float)(num614 + num613);
				center.Y -= (float)num617 * num618 / 2f;
				center.Y += 2f;
				int num335 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), center.X, center.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
			}
			if (Projectile.ai[0] <= 0f)
			{
				float num622 = 0.104719758f;
				float num623 = (float)Projectile.width / 5f;
				if (dukeFish)
				{
					num623 *= 2f;
				}
				float num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X - num624 * (float)(-(float)Projectile.direction);
				Projectile.ai[0] -= 1f;
				num624 = (float)(Math.Cos((double)(num622 * -(double)Projectile.ai[0])) - 0.5) * num623;
				Projectile.position.X = Projectile.position.X + num624 * (float)(-(float)Projectile.direction);
				return;
			}
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(53, Main.DiscoG, 255, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}