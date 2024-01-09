﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class InfernadoMarkFriendly : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mark");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
        }
        
        public override void AI()
        {
        	if (Projectile.ai[1] > 0f)
			{
				int num625 = (int)Projectile.ai[1] - 1;
				if (num625 < 255)
				{
					Projectile.localAI[0] += 1f;
					if (Projectile.localAI[0] > 10f)
					{
						int num626 = 6;
						for (int num627 = 0; num627 < num626; num627++)
						{
							Vector2 vector45 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
							vector45 = vector45.RotatedBy((double)(num627 - (num626 / 2 - 1)) * 3.1415926535897931 / (double)((float)num626), default(Vector2)) + Projectile.Center;
							Vector2 value15 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
							int num628 = Dust.NewDust(vector45 + value15, 0, 0, DustID.CopperCoin, value15.X * 2f, value15.Y * 2f, 100, default(Color), 1.4f);
							Main.dust[num628].noGravity = true;
							Main.dust[num628].noLight = true;
							Main.dust[num628].velocity /= 4f;
							Main.dust[num628].velocity -= Projectile.velocity;
						}
						Projectile.alpha -= 5;
						if (Projectile.alpha < 100)
						{
							Projectile.alpha = 100;
						}
					}
					Vector2 value16 = Main.player[num625].Center - Projectile.Center;
					float num629 = 4f;
					num629 += Projectile.localAI[0] / 20f;
					Projectile.velocity = Vector2.Normalize(value16) * num629;
					if (value16.Length() < 50f)
					{
						Projectile.Kill();
					}
				}
			}
			else
			{
				float num630 = 0.209439516f;
				float num631 = 4f;
				float num632 = (float)(Math.Cos((double)(num630 * Projectile.ai[0])) - 0.5) * num631;
				Projectile.velocity.Y = Projectile.velocity.Y - num632;
				Projectile.ai[0] += 1f;
				num632 = (float)(Math.Cos((double)(num630 * Projectile.ai[0])) - 0.5) * num631;
				Projectile.velocity.Y = Projectile.velocity.Y + num632;
				Projectile.localAI[0] += 1f;
				if (Projectile.localAI[0] > 10f)
				{
					Projectile.alpha -= 5;
					if (Projectile.alpha < 100)
					{
						Projectile.alpha = 100;
					}
				}
			}
			if (Projectile.wet)
			{
				Projectile.position.Y = Projectile.position.Y - 16f;
				Projectile.Kill();
				return;
			}
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
			int num226 = 36;
			for (int num227 = 0; num227 < num226; num227++)
			{
				Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
				vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
				Vector2 vector7 = vector6 - Projectile.Center;
				int num228 = Dust.NewDust(vector6 + vector7, 0, 0, DustID.CopperCoin, vector7.X * 2f, vector7.Y * 2f, 100, default(Color), 1.4f);
				Main.dust[num228].noGravity = true;
				Main.dust[num228].noLight = true;
				Main.dust[num228].velocity = vector7;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				int num231 = (int)(Projectile.Center.Y / 16f);
				int num232 = (int)(Projectile.Center.X / 16f);
				int num233 = 100;
				if (num232 < 10) 
				{
					num232 = 10;
				}
				if (num232 > Main.maxTilesX - 10) 
				{
					num232 = Main.maxTilesX - 10;
				}
				if (num231 < 10) 
				{
					num231 = 10;
				}
				if (num231 > Main.maxTilesY - num233 - 10) 
				{
					num231 = Main.maxTilesY - num233 - 10;
				}
				for (int num234 = num231; num234 < num231 + num233; num234++) 
				{
					Tile tile = Main.tile[num232, num234];
					if (tile.HasTile && (Main.tileSolid[(int)tile.TileType] || tile.LiquidAmount != 0)) 
					{
						num231 = num234;
						break;
					}
				}
				int num236 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), (float)(num232 * 16 + 8), (float)(num231 * 16 - 24), 0f, 0f, Mod.Find<ModProjectile>("InfernadoFriendly").Type, 1000, 15f, Main.myPlayer, 16f, 16f);
				Main.projectile[num236].netUpdate = true;
			}
        }
    }
}