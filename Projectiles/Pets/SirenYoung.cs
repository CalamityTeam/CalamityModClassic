using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Pets
{
	public class SirenYoung : ModProjectile
	{
		private bool underwater = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Siren");
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (!player.active)
			{
				Projectile.active = false;
				return;
			}
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (player.dead)
			{
				modPlayer.sirenPet = false;
			}
			if (modPlayer.sirenPet)
			{
				Projectile.timeLeft = 2;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 6)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}
			underwater = Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
			if (underwater)
			{
				Projectile.width = 30;
				Projectile.height = 30;
				if (Projectile.localAI[0] == 0f)
				{
					Lighting.AddLight(Projectile.Center, 2.5f, 2f, 0f); //4.5
				}
				else
				{
					Lighting.AddLight(Projectile.Center, 1.65f, 1.32f, 0f); //3
				}
			}
			else
			{
				Projectile.width = 54;
				Projectile.height = 54;
				Lighting.AddLight(Projectile.Center, 0.825f, 0.66f, 0f); //1.5
				Vector2 vector54 = player.Center;
				vector54.X -= (float)((15 + player.width / 2) * player.direction);
				vector54.X -= (float)(40 * player.direction);
				if (Projectile.ai[0] == 1f)
				{
					Projectile.tileCollide = false;
					float num663 = 0.2f;
					float num664 = 10f;
					int num665 = 200;
					if (num664 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
					{
						num664 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
					}
					Vector2 vector58 = vector54 - Projectile.Center;
					float num666 = vector58.Length();
					if (num666 > 2000f)
					{
						Projectile.position = player.Center - new Vector2((float)Projectile.width, (float)Projectile.height) / 2f;
					}
					if (num666 < (float)num665 && player.velocity.Y == 0f && Projectile.position.Y + (float)Projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
					{
						Projectile.ai[0] = 0f;
						Projectile.netUpdate = true;
						if (Projectile.velocity.Y < -6f)
						{
							Projectile.velocity.Y = -6f;
						}
					}
					if (num666 >= 60f)
					{
						vector58.Normalize();
						vector58 *= num664;
						if (Projectile.velocity.X < vector58.X)
						{
							Projectile.velocity.X = Projectile.velocity.X + num663;
							if (Projectile.velocity.X < 0f)
							{
								Projectile.velocity.X = Projectile.velocity.X + num663 * 1.5f;
							}
						}
						if (Projectile.velocity.X > vector58.X)
						{
							Projectile.velocity.X = Projectile.velocity.X - num663;
							if (Projectile.velocity.X > 0f)
							{
								Projectile.velocity.X = Projectile.velocity.X - num663 * 1.5f;
							}
						}
						if (Projectile.velocity.Y < vector58.Y)
						{
							Projectile.velocity.Y = Projectile.velocity.Y + num663;
							if (Projectile.velocity.Y < 0f)
							{
								Projectile.velocity.Y = Projectile.velocity.Y + num663 * 1.5f;
							}
						}
						if (Projectile.velocity.Y > vector58.Y)
						{
							Projectile.velocity.Y = Projectile.velocity.Y - num663;
							if (Projectile.velocity.Y > 0f)
							{
								Projectile.velocity.Y = Projectile.velocity.Y - num663 * 1.5f;
							}
						}
					}
					if (Projectile.velocity.X != 0f)
					{
						Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
					}
				}
				if (Projectile.ai[0] == 2f)
				{
					Projectile.friendly = true;
					Projectile.spriteDirection = Projectile.direction;
					Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
					float[] var_2_1C896_cp_0 = Projectile.ai;
					int var_2_1C896_cp_1 = 1;
					float num73 = var_2_1C896_cp_0[var_2_1C896_cp_1];
					var_2_1C896_cp_0[var_2_1C896_cp_1] = num73 - 1f;
					if (Projectile.ai[1] <= 0f)
					{
						Projectile.ai[1] = 0f;
						Projectile.ai[0] = 0f;
						Projectile.friendly = false;
						Projectile.netUpdate = true;
						return;
					}
				}
				if (Projectile.ai[0] == 0f)
				{
					float num671 = 200f;
					if (player.rocketDelay2 > 0)
					{
						Projectile.ai[0] = 1f;
						Projectile.netUpdate = true;
					}
					Vector2 vector59 = vector54 - Projectile.Center;
					if (vector59.Length() > 2000f)
					{
						Projectile.position = vector54 - new Vector2((float)Projectile.width, (float)Projectile.height) / 2f;
					}
					else if (vector59.Length() > num671 || Math.Abs(vector59.Y) > 300f)
					{
						Projectile.ai[0] = 1f;
						Projectile.netUpdate = true;
						if (Projectile.velocity.Y > 0f && vector59.Y < 0f)
						{
							Projectile.velocity.Y = 0f;
						}
						if (Projectile.velocity.Y < 0f && vector59.Y > 0f)
						{
							Projectile.velocity.Y = 0f;
						}
					}
					Projectile.tileCollide = true;
					float num672 = 0.5f;
					float num673 = 4f;
					float num674 = 4f;
					float num675 = 0.1f;
					if (num674 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
					{
						num674 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
						num672 = 0.7f;
					}
					int num676 = 0;
					bool flag29 = false;
					float num677 = vector54.X - Projectile.Center.X;
					if (Math.Abs(num677) > 5f)
					{
						if (num677 < 0f)
						{
							num676 = -1;
							if (Projectile.velocity.X > -num673)
							{
								Projectile.velocity.X = Projectile.velocity.X - num672;
							}
							else
							{
								Projectile.velocity.X = Projectile.velocity.X - num675;
							}
						}
						else
						{
							num676 = 1;
							if (Projectile.velocity.X < num673)
							{
								Projectile.velocity.X = Projectile.velocity.X + num672;
							}
							else
							{
								Projectile.velocity.X = Projectile.velocity.X + num675;
							}
						}
						flag29 = true;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X * 0.9f;
						if (Math.Abs(Projectile.velocity.X) < num672 * 2f)
						{
							Projectile.velocity.X = 0f;
						}
					}
					if (num676 != 0)
					{
						int num678 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
						int num679 = (int)Projectile.position.Y / 16;
						num678 += num676;
						num678 += (int)Projectile.velocity.X;
						int num3;
						for (int num680 = num679; num680 < num679 + Projectile.height / 16 + 1; num680 = num3 + 1)
						{
							if (WorldGen.SolidTile(num678, num680))
							{
								flag29 = true;
							}
							num3 = num680;
						}
					}
					if (Projectile.velocity.X != 0f)
					{
						flag29 = true;
					}
					Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY, 1, false, 0);
					if (Projectile.velocity.Y == 0f && flag29)
					{
						int num3;
						for (int num681 = 0; num681 < 3; num681 = num3 + 1)
						{
							int num682 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
							if (num681 == 0)
							{
								num682 = (int)Projectile.position.X / 16;
							}
							if (num681 == 2)
							{
								num682 = (int)(Projectile.position.X + (float)Projectile.width) / 16;
							}
							int num683 = (int)(Projectile.position.Y + (float)Projectile.height) / 16;
							if (WorldGen.SolidTile(num682, num683) || Main.tile[num682, num683].IsHalfBlock || Main.tile[num682, num683].Slope > 0 || (TileID.Sets.Platforms[(int)Main.tile[num682, num683].TileType] && Main.tile[num682, num683].HasTile && !Main.tile[num682, num683].IsActuated))
							{
								try
								{
									num682 = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
									num683 = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
									num682 += num676;
									num682 += (int)Projectile.velocity.X;
									if (!WorldGen.SolidTile(num682, num683 - 1) && !WorldGen.SolidTile(num682, num683 - 2))
									{
										Projectile.velocity.Y = -5.1f;
									}
									else if (!WorldGen.SolidTile(num682, num683 - 2))
									{
										Projectile.velocity.Y = -7.1f;
									}
									else if (WorldGen.SolidTile(num682, num683 - 5))
									{
										Projectile.velocity.Y = -11.1f;
									}
									else if (WorldGen.SolidTile(num682, num683 - 4))
									{
										Projectile.velocity.Y = -10.1f;
									}
									else
									{
										Projectile.velocity.Y = -9.1f;
									}
								}
								catch
								{
									Projectile.velocity.Y = -9.1f;
								}
							}
							num3 = num681;
						}
					}
					if (Projectile.velocity.X > num674)
					{
						Projectile.velocity.X = num674;
					}
					if (Projectile.velocity.X < -num674)
					{
						Projectile.velocity.X = -num674;
					}
					if (Projectile.velocity.X < 0f)
					{
						Projectile.direction = -1;
					}
					if (Projectile.velocity.X > 0f)
					{
						Projectile.direction = 1;
					}
					if (Projectile.velocity.X > num672 && num676 == 1)
					{
						Projectile.direction = 1;
					}
					if (Projectile.velocity.X < -num672 && num676 == -1)
					{
						Projectile.direction = -1;
					}
					Projectile.spriteDirection = Projectile.direction;
					Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
					if (Projectile.velocity.Y > 10f)
					{
						Projectile.velocity.Y = 10f;
					}
				}
				return;
			}
			float num23 = 0.2f;
			float num24 = 5f;
			Projectile.tileCollide = false;
			Vector2 vector4 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num25 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector4.X;
			float num26 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].gfxOffY + (float)(Main.player[Projectile.owner].height / 2) - vector4.Y;
			if (Main.player[Projectile.owner].controlLeft)
			{
				num25 -= 120f;
			}
			else if (Main.player[Projectile.owner].controlRight)
			{
				num25 += 120f;
			}
			if (Main.player[Projectile.owner].controlDown)
			{
				num26 += 120f;
			}
			else
			{
				if (Main.player[Projectile.owner].controlUp)
				{
					num26 -= 120f;
				}
				num26 -= 60f;
			}
			float num27 = (float)Math.Sqrt((double)(num25 * num25 + num26 * num26));
			if (num27 > 1000f)
			{
				Projectile.position.X = Projectile.position.X + num25;
				Projectile.position.Y = Projectile.position.Y + num26;
			}
			if (Projectile.localAI[0] == 1f)
			{
				if (num27 < 10f && Math.Abs(Main.player[Projectile.owner].velocity.X) + Math.Abs(Main.player[Projectile.owner].velocity.Y) < num24 && Main.player[Projectile.owner].velocity.Y == 0f)
				{
					Projectile.localAI[0] = 0f;
				}
				num24 = 12f;
				if (num27 < num24)
				{
					Projectile.velocity.X = num25;
					Projectile.velocity.Y = num26;
				}
				else
				{
					num27 = num24 / num27;
					Projectile.velocity.X = num25 * num27;
					Projectile.velocity.Y = num26 * num27;
				}
				if ((double)Projectile.velocity.X > 0.5)
				{
					Projectile.direction = -1;
				}
				else if ((double)Projectile.velocity.X < -0.5)
				{
					Projectile.direction = 1;
				}
				Projectile.spriteDirection = Projectile.direction;
				Projectile.rotation = Projectile.velocity.X * 0.05f;
				return;
			}
			if (num27 > 200f)
			{
				Projectile.localAI[0] = 1f;
			}
			if ((double)Projectile.velocity.X > 0.5)
			{
				Projectile.direction = -1;
			}
			else if ((double)Projectile.velocity.X < -0.5)
			{
				Projectile.direction = 1;
			}
			Projectile.spriteDirection = Projectile.direction;
			if (num27 < 10f)
			{
				Projectile.velocity.X = num25;
				Projectile.velocity.Y = num26;
				Projectile.rotation = Projectile.velocity.X * 0.05f;
				if (num27 < num24)
				{
					Projectile.position += Projectile.velocity;
					Projectile.velocity *= 0f;
					num23 = 0f;
				}
				Projectile.direction = -Main.player[Projectile.owner].direction;
			}
			num27 = num24 / num27;
			num25 *= num27;
			num26 *= num27;
			if (Projectile.velocity.X < num25)
			{
				Projectile.velocity.X = Projectile.velocity.X + num23;
				if (Projectile.velocity.X < 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.99f;
				}
			}
			if (Projectile.velocity.X > num25)
			{
				Projectile.velocity.X = Projectile.velocity.X - num23;
				if (Projectile.velocity.X > 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.99f;
				}
			}
			if (Projectile.velocity.Y < num26)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + num23;
				if (Projectile.velocity.Y < 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y * 0.99f;
				}
			}
			if (Projectile.velocity.Y > num26)
			{
				Projectile.velocity.Y = Projectile.velocity.Y - num23;
				if (Projectile.velocity.Y > 0f)
				{
					Projectile.velocity.Y = Projectile.velocity.Y * 0.99f;
				}
			}
			if (Projectile.velocity.X != 0f || Projectile.velocity.Y != 0f)
			{
				Projectile.rotation = Projectile.velocity.X * 0.05f;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture2D13 = (underwater ? TextureAssets.Projectile[Projectile.type].Value : ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Pets/SirenFlopping").Value);
			int num214 = (underwater ? TextureAssets.Projectile[Projectile.type].Value.Height : 256) / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}