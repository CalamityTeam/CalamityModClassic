using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.SunkenSea
{
	public class Shellfish : ModProjectile
	{
		private int playerStill = 0;
		private bool fly = false;
		private bool spawnDust = true;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shellfish");
			Main.projFrames[Projectile.type] = 2;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 24;
			Projectile.netImportant = true;
			Projectile.friendly = true;
			Projectile.minionSlots = 2;
			Projectile.timeLeft = 18000;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.minion = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return true;
		}

		public override void AI()
		{
			if (spawnDust)
			{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num501 = 20;
				for (int num502 = 0; num502 < num501; num502++)
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 33, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
				spawnDust = false;
			}
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("Shellfish").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.AddBuff(Mod.Find<ModBuff>("Shellfish").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.shellfish = false;
				}
				if (modPlayer.shellfish)
				{
					Projectile.timeLeft = 2;
				}
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter > 3)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 1)
			{
				Projectile.frame = 0;
			}
			if (Projectile.ai[0] == 0f)
			{
				Projectile.damage = 70;
				if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
				{
					int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
						Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
						Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
					Projectile.damage = damage2;
				}
				float[] var0 = Projectile.ai;
				int var1 = 1;
				float num73 = var0[var1];
				var0[var1] = num73 + 1f;
				Vector2 vector46 = Projectile.position;
				if (!fly)
				{
					Projectile.tileCollide = true;
					Vector2 center2 = Projectile.Center;
					Vector2 vector48 = player.Center - center2;
					float playerDistance = vector48.Length();
					if (Projectile.velocity.Y == 0f && (Projectile.velocity.X != 0f || playerDistance > 200f))
					{
						switch (Main.rand.Next(1, 3))
						{

							case 1:
								Projectile.velocity.Y -= 5f;
								break;

							case 2:
								Projectile.velocity.Y -= 7.5f;
								break;

							case 3:
								Projectile.velocity.Y -= 10f;
								break;
						}
					}
					Projectile.velocity.Y += 0.3f;
					float num633 = 1000f;
					bool chaseNPC = false;
					float npcPositionX = 0f;
					for (int num645 = 0; num645 < 200; num645++)
					{
						NPC npcTarget = Main.npc[num645];
						if (npcTarget.CanBeChasedBy(Projectile, false))
						{
							float num646 = Vector2.Distance(npcTarget.Center, Projectile.Center);
							if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !chaseNPC))
							{
								num633 = num646;
								vector46 = npcTarget.Center;
								npcPositionX = npcTarget.position.X;
								chaseNPC = true;
							}
						}
					}
					if (chaseNPC)
					{
						if (npcPositionX - Projectile.position.X > 0f)
						{
							switch (Main.rand.Next(1, 2))
							{

								case 1:
									Projectile.velocity.X += 0.15f;
									break;

								case 2:
									Projectile.velocity.X += 0.20f;
									break;
							}

							if (Projectile.velocity.X > 8f)
							{
								Projectile.velocity.X = 8f;
							}
						}
						else
						{
							switch (Main.rand.Next(1, 2))
							{

								case 1:
									Projectile.velocity.X -= 0.15f;
									break;

								case 2:
									Projectile.velocity.X -= 0.20f;
									break;
							}

							if (Projectile.velocity.X < -8f)
							{
								Projectile.velocity.X = -8f;
							}
						}
					}
					else
					{
						if (playerDistance > 800f)
						{
							fly = true;
							Projectile.velocity.X = 0f;
							Projectile.velocity.Y = 0f;
							Projectile.tileCollide = false;
						}
						if (playerDistance > 200f)
						{
							if (player.position.X - Projectile.position.X > 0f)
							{
								switch (Main.rand.Next(1, 3))
								{
									case 1:
										Projectile.velocity.X += 0.05f;
										break;

									case 2:
										Projectile.velocity.X += 0.10f;
										break;

									case 3:
										Projectile.velocity.X += 0.15f;
										break;
								}

								if (Projectile.velocity.X > 6f)
								{
									Projectile.velocity.X = 6f;
								}
							}
							else
							{
								switch (Main.rand.Next(1, 3))
								{
									case 1:
										Projectile.velocity.X -= 0.05f;
										break;

									case 2:
										Projectile.velocity.X -= 0.10f;
										break;

									case 3:
										Projectile.velocity.X -= 0.15f;
										break;
								}

								if (Projectile.velocity.X < -6f)
								{
									Projectile.velocity.X = -6f;
								}
							}
						}
						if (playerDistance < 200f)
						{
							if (Projectile.velocity.X != 0f)
							{
								if (Projectile.velocity.X > 0.5f)
								{
									switch (Main.rand.Next(1, 3))
									{
										case 1:
											Projectile.velocity.X -= 0.05f;
											break;

										case 2:
											Projectile.velocity.X -= 0.10f;
											break;

										case 3:
											Projectile.velocity.X -= 0.15f;
											break;
									}
								}
								else if (Projectile.velocity.X < -0.5f)
								{
									switch (Main.rand.Next(1, 3))
									{
										case 1:
											Projectile.velocity.X += 0.05f;
											break;

										case 2:
											Projectile.velocity.X += 0.10f;
											break;

										case 3:
											Projectile.velocity.X += 0.15f;
											break;
									}
								}
								else if (Projectile.velocity.X < 0.5f && Projectile.velocity.X > -0.5f)
								{
									Projectile.velocity.X = 0f;
								}
							}
						}
					}
				}
				else if (fly)
				{
					Vector2 center2 = Projectile.Center;
					Vector2 vector48 = player.Center - center2 + new Vector2(0f, 0f);
					float playerDistance = vector48.Length();
					vector48.Normalize();
					vector48 *= 14f;
					Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;

					Projectile.rotation = Projectile.velocity.X * 0.03f;
					if (playerDistance > 1500f)
					{
						Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
						Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
						Projectile.netUpdate = true;
					}
					if (playerDistance < 100f)
					{
						if (player.velocity.Y == 0f)
						{
							++playerStill;
						}
						else
						{
							playerStill = 0;
						}
						if (playerStill > 30 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
						{
							fly = false;
							Projectile.tileCollide = true;
							Projectile.rotation = 0;
							Projectile.velocity.X *= 0.30f;
							Projectile.velocity.Y *= 0.30f;
						}
					}
				}
				if ((double)Projectile.velocity.X > 0.25)
				{
					Projectile.spriteDirection = -1;
				}
				else if ((double)Projectile.velocity.X < -0.25)
				{
					Projectile.spriteDirection = 1;
				}
			}
			if (Projectile.ai[0] == 1f)
			{
				Projectile.rotation = 0;
				Projectile.tileCollide = false;
				int num988 = 10;
				bool flag54 = false;
				bool flag55 = false;
				float[] var0 = Projectile.localAI;
				int var1 = 0;
				float num73 = var0[var1];
				var0[var1] = num73 + 1f;
				if (Projectile.localAI[0] % 30f == 0f)
				{
					flag55 = true;
				}
				int num989 = (int)Projectile.ai[1];
				if (Projectile.localAI[0] >= (float)(60000 * num988)) //tryna make it stay on there "forever" without glitching
				{
					flag54 = true;
				}
				else if (num989 < 0 || num989 >= 200)
				{
					flag54 = true;
				}
				else if (Main.npc[num989].active && !Main.npc[num989].dontTakeDamage && Main.npc[num989].defense < 9999)
				{
					Projectile.Center = Main.npc[num989].Center - Projectile.velocity * 2f;
					Projectile.gfxOffY = Main.npc[num989].gfxOffY;
					if (flag55)
					{
						Main.npc[num989].HitEffect(0, 1.0);
					}
				}
				else
				{
					flag54 = true;
				}
				if (flag54)
				{
					Projectile.ai[0] = 0f;
					Projectile.velocity.X = 0f;
					Projectile.velocity.Y = 0f;
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			Rectangle myRect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
			if (Projectile.owner == Main.myPlayer)
			{
				for (int i = 0; i < 200; i++)
				{
					if (((Main.npc[i].active && !Main.npc[i].dontTakeDamage && Main.npc[i].defense < 9999)) &&
						((Projectile.friendly && (!Main.npc[i].friendly || Projectile.type == 318 || (Main.npc[i].type == 22 && Projectile.owner < 255 && Main.player[Projectile.owner].killGuide) || (Main.npc[i].type == 54 && Projectile.owner < 255 && Main.player[Projectile.owner].killClothier))) ||
						(Projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (Projectile.owner < 0 || Main.npc[i].immune[Projectile.owner] == 0 || Projectile.maxPenetrate == 1))
					{
						if (Main.npc[i].noTileCollide || !Projectile.ownerHitCheck)
						{
							bool flag3;
							if (Main.npc[i].type == 414)
							{
								Rectangle rect = Main.npc[i].getRect();
								int num5 = 8;
								rect.X -= num5;
								rect.Y -= num5;
								rect.Width += num5 * 2;
								rect.Height += num5 * 2;
								flag3 = Projectile.Colliding(myRect, rect);
							}
							else
							{
								flag3 = Projectile.Colliding(myRect, Main.npc[i].getRect());
							}
							if (flag3)
							{
								if (Main.npc[i].reflectsProjectiles && Projectile.CanBeReflected())
								{
									Main.npc[i].ReflectProjectile(Projectile);
									return;
								}
								Projectile.ai[0] = 1f;
								Projectile.ai[1] = (float)i;
								Projectile.velocity = (Main.npc[i].Center - Projectile.Center) * 0.75f;
								Projectile.netUpdate = true;
								Projectile.StatusNPC(i);
								Projectile.damage = 0;
								int num28 = 10;
								Point[] array2 = new Point[num28];
								int num29 = 0;
								for (int l = 0; l < 1000; l++)
								{
									if (l != Projectile.whoAmI && Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type && Main.projectile[l].ai[0] == 1f && Main.projectile[l].ai[1] == (float)i)
									{
										array2[num29++] = new Point(l, Main.projectile[l].timeLeft);
										if (num29 >= array2.Length)
										{
											break;
										}
									}
								}
								if (num29 >= array2.Length)
								{
									int num30 = 0;
									for (int m = 1; m < array2.Length; m++)
									{
										if (array2[m].Y < array2[num30].Y)
										{
											num30 = m;
										}
									}
									Main.projectile[array2[num30].X].Kill();
								}
							}
						}
					}
				}
			}
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return null;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.penetrate == 0)
			{
				Projectile.Kill();
			}
			return false;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.buffImmune[Mod.Find<ModBuff>("ShellfishEating").Type] = false;
			if (target.type == Mod.Find<ModNPC>("CeaselessVoid").Type || target.type == Mod.Find<ModNPC>("EidolonWyrmHeadHuge").Type)
			{
				target.buffImmune[Mod.Find<ModBuff>("ShellfishEating").Type] = true;
			}
			target.AddBuff(Mod.Find<ModBuff>("ShellfishEating").Type, 600000);
		}
	}
}