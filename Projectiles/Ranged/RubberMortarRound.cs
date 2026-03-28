using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class RubberMortarRound : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Round");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
        	Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
			{
				Projectile.tileCollide = false;
				Projectile.ai[1] = 0f;
				Projectile.alpha = 255;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.width = 200;
				Projectile.height = 200;
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
				Projectile.knockBack = 10f;
			}
			else
			{
				if (Math.Abs(Projectile.velocity.X) >= 8f || Math.Abs(Projectile.velocity.Y) >= 8f)
				{
					for (int num246 = 0; num246 < 2; num246++)
					{
						float num247 = 0f;
						float num248 = 0f;
						if (num246 == 1)
						{
							num247 = Projectile.velocity.X * 0.5f;
							num248 = Projectile.velocity.Y * 0.5f;
						}
						int num249 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num247, Projectile.position.Y + 3f + num248) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
						Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
						Main.dust[num249].velocity *= 0.2f;
						Main.dust[num249].noGravity = true;
						num249 = Dust.NewDust(new Vector2(Projectile.position.X + 3f + num247, Projectile.position.Y + 3f + num248) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
						Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
						Main.dust[num249].velocity *= 0.05f;
					}
				}
			}
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int num621 = 0; num621 < 40; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 70; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
			for (int num625 = 0; num625 < 3; num625++)
			{
				float scaleFactor10 = 0.33f;
				if (num625 == 1)
				{
					scaleFactor10 = 0.66f;
				}
				if (num625 == 2)
				{
					scaleFactor10 = 1f;
				}
				int num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13AB6_cp_0 = Main.gore[num626];
				expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
				Gore expr_13AD6_cp_0 = Main.gore[num626];
				expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
				num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13B79_cp_0 = Main.gore[num626];
				expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
				Gore expr_13B99_cp_0 = Main.gore[num626];
				expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
				num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13C3C_cp_0 = Main.gore[num626];
				expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
				Gore expr_13C5C_cp_0 = Main.gore[num626];
				expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
				num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13CFF_cp_0 = Main.gore[num626];
				expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
				Gore expr_13D1F_cp_0 = Main.gore[num626];
				expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				int num814 = 5;
				int num815 = (int)(Projectile.position.X / 16f - (float)num814);
				int num816 = (int)(Projectile.position.X / 16f + (float)num814);
				int num817 = (int)(Projectile.position.Y / 16f - (float)num814);
				int num818 = (int)(Projectile.position.Y / 16f + (float)num814);
				if (num815 < 0)
				{
					num815 = 0;
				}
				if (num816 > Main.maxTilesX)
				{
					num816 = Main.maxTilesX;
				}
				if (num817 < 0)
				{
					num817 = 0;
				}
				if (num818 > Main.maxTilesY)
				{
					num818 = Main.maxTilesY;
				}
				bool flag3 = false;
				for (int num819 = num815; num819 <= num816; num819++)
				{
					for (int num820 = num817; num820 <= num818; num820++)
					{
						float num821 = Math.Abs((float)num819 - Projectile.position.X / 16f);
						float num822 = Math.Abs((float)num820 - Projectile.position.Y / 16f);
						double num823 = Math.Sqrt((double)(num821 * num821 + num822 * num822));
						if (num823 < (double)num814 && Main.tile[num819, num820] != null && Main.tile[num819, num820].WallType == 0)
						{
							flag3 = true;
							break;
						}
					}
				}
				AchievementsHelper.CurrentlyMining = true;
				for (int num824 = num815; num824 <= num816; num824++)
				{
					for (int num825 = num817; num825 <= num818; num825++)
					{
						float num826 = Math.Abs((float)num824 - Projectile.position.X / 16f);
						float num827 = Math.Abs((float)num825 - Projectile.position.Y / 16f);
						double num828 = Math.Sqrt((double)(num826 * num826 + num827 * num827));
						if (num828 < (double)num814)
						{
							if (Main.tile[num824, num825] != null && Main.tile[num824, num825].HasTile && Main.tile[num824, num825].TileType != (ushort)Mod.Find<ModTile>("AbyssGravel").Type)
							{
								WorldGen.KillTile(num824, num825, false, false, false);
								if (!Main.tile[num824, num825].HasTile && Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 0, (float)num824, (float)num825, 0f, 0, 0, 0);
								}
							}
							for (int num829 = num824 - 1; num829 <= num824 + 1; num829++)
							{
								for (int num830 = num825 - 1; num830 <= num825 + 1; num830++)
								{
									if (Main.tile[num829, num830] != null && Main.tile[num829, num830].WallType > 0 && Main.tile[num829, num830].WallType != (byte)Mod.Find<ModWall>("AbyssGravelWall").Type && flag3)
									{
										WorldGen.KillWall(num829, num830, false);
										if (Main.tile[num829, num830].WallType == 0 && Main.netMode != 0)
										{
											NetMessage.SendData(17, -1, -1, null, 2, (float)num829, (float)num830, 0f, 0, 0, 0);
										}
									}
								}
							}
						}
					}
				}
				AchievementsHelper.CurrentlyMining = false;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(29, -1, -1, null, Projectile.identity, (float)Projectile.owner, 0f, 0f, 0, 0, 0);
				}
				if (!Projectile.noDropItem)
				{
					int num831 = -1;
					if (Main.netMode == 1 && num831 >= 0)
					{
						NetMessage.SendData(21, -1, -1, null, num831, 1f, 0f, 0f, 0, 0, 0);
					}
				}
			}
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
                Projectile.active = false;
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
                Projectile.velocity *= 1.25f;
            }
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int num621 = 0; num621 < 40; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 70; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
			for (int num625 = 0; num625 < 3; num625++)
			{
				float scaleFactor10 = 0.33f;
				if (num625 == 1)
				{
					scaleFactor10 = 0.66f;
				}
				if (num625 == 2)
				{
					scaleFactor10 = 1f;
				}
				int num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13AB6_cp_0 = Main.gore[num626];
				expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
				Gore expr_13AD6_cp_0 = Main.gore[num626];
				expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
				num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13B79_cp_0 = Main.gore[num626];
				expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
				Gore expr_13B99_cp_0 = Main.gore[num626];
				expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
				num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13C3C_cp_0 = Main.gore[num626];
				expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
				Gore expr_13C5C_cp_0 = Main.gore[num626];
				expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
				num626 = Gore.NewGore(Projectile.GetSource_FromThis(null), new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Gore expr_13CFF_cp_0 = Main.gore[num626];
				expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
				Gore expr_13D1F_cp_0 = Main.gore[num626];
				expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				int num814 = 5;
				int num815 = (int)(Projectile.position.X / 16f - (float)num814);
				int num816 = (int)(Projectile.position.X / 16f + (float)num814);
				int num817 = (int)(Projectile.position.Y / 16f - (float)num814);
				int num818 = (int)(Projectile.position.Y / 16f + (float)num814);
				if (num815 < 0)
				{
					num815 = 0;
				}
				if (num816 > Main.maxTilesX)
				{
					num816 = Main.maxTilesX;
				}
				if (num817 < 0)
				{
					num817 = 0;
				}
				if (num818 > Main.maxTilesY)
				{
					num818 = Main.maxTilesY;
				}
				bool flag3 = false;
				for (int num819 = num815; num819 <= num816; num819++)
				{
					for (int num820 = num817; num820 <= num818; num820++)
					{
						float num821 = Math.Abs((float)num819 - Projectile.position.X / 16f);
						float num822 = Math.Abs((float)num820 - Projectile.position.Y / 16f);
						double num823 = Math.Sqrt((double)(num821 * num821 + num822 * num822));
						if (num823 < (double)num814 && Main.tile[num819, num820] != null && Main.tile[num819, num820].WallType == 0)
						{
							flag3 = true;
							break;
						}
					}
				}
				AchievementsHelper.CurrentlyMining = true;
				for (int num824 = num815; num824 <= num816; num824++)
				{
					for (int num825 = num817; num825 <= num818; num825++)
					{
						float num826 = Math.Abs((float)num824 - Projectile.position.X / 16f);
						float num827 = Math.Abs((float)num825 - Projectile.position.Y / 16f);
						double num828 = Math.Sqrt((double)(num826 * num826 + num827 * num827));
						if (num828 < (double)num814)
						{
							if (Main.tile[num824, num825] != null && Main.tile[num824, num825].HasTile)
							{
								WorldGen.KillTile(num824, num825, false, false, false);
								if (!Main.tile[num824, num825].HasTile && Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 0, (float)num824, (float)num825, 0f, 0, 0, 0);
								}
							}
							for (int num829 = num824 - 1; num829 <= num824 + 1; num829++)
							{
								for (int num830 = num825 - 1; num830 <= num825 + 1; num830++)
								{
									if (Main.tile[num829, num830] != null && Main.tile[num829, num830].WallType > 0 && flag3)
									{
										WorldGen.KillWall(num829, num830, false);
										if (Main.tile[num829, num830].WallType == 0 && Main.netMode != 0)
										{
											NetMessage.SendData(17, -1, -1, null, 2, (float)num829, (float)num830, 0f, 0, 0, 0);
										}
									}
								}
							}
						}
					}
				}
				AchievementsHelper.CurrentlyMining = false;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(29, -1, -1, null, Projectile.identity, (float)Projectile.owner, 0f, 0f, 0, 0, 0);
				}
				if (!Projectile.noDropItem)
				{
					int num831 = -1;
					if (Main.netMode == 1 && num831 >= 0)
					{
						NetMessage.SendData(21, -1, -1, null, num831, 1f, 0f, 0f, 0, 0, 0);
					}
				}
			}
        }
    }
}