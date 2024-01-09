﻿using System;
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
    public class SkyFlareRevenge : ModProjectile
    {
    	public int blowTimer = 0;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Flare");
			Main.projFrames[Projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            CooldownSlot = 1;
        }

        public override void AI()
        {
        	int addStuff = Main.rand.Next(5);
        	blowTimer += addStuff;
        	if (blowTimer >= 900)
        	{
        		Projectile.Kill();
        	}
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 5)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 4)
			{
			   Projectile.frame = 0;
			}
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(255, Main.DiscoG, 53, Projectile.alpha);
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
				int num235 = Main.expertMode ? 300 : 350;
				int num236 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), (float)(num232 * 16 + 8), (float)(num231 * 16 - 24), 0f, 0f, Mod.Find<ModProjectile>("InfernadoRevenge").Type, num235, 4f, Main.myPlayer, 16f, 36f);
				Main.projectile[num236].netUpdate = true;
			}
        }
    }
}