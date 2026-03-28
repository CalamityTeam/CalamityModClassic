using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class Flounder : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flounder");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			NPC.chaseable = false;
			NPC.damage = 10;
			NPC.width = 42;
			NPC.height = 32;
			NPC.defense = 15;
			NPC.lifeMax = 40;
			NPC.aiStyle = -1;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.value = Item.buyPrice(0, 0, 0, 80);
			NPC.HitSound = SoundID.NPCHit50;
			NPC.DeathSound = SoundID.NPCDeath53;
			NPC.knockBackResist = 0.35f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("FlounderBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<Sulphur>().Type };
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Relatively simple fish that launch acid towards their prey in order to subdue it.")
			});
		}

		public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			int num = 200;
			if (NPC.ai[2] == 0f)
			{
				NPC.alpha = num;
				NPC.TargetClosest(true);
				if (!Main.player[NPC.target].dead && (Main.player[NPC.target].Center - NPC.Center).Length() < 170f &&
					Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					NPC.ai[2] = -16f;
				}
				if (NPC.velocity.X != 0f || NPC.velocity.Y < 0f || NPC.velocity.Y > 2f || NPC.justHit)
				{
					NPC.ai[2] = -16f;
				}
				return;
			}
			if (NPC.ai[2] < 0f)
			{
				if (NPC.alpha > 0)
				{
					NPC.alpha -= num / 16;
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
				}
				NPC.ai[2] += 1f;
				if (NPC.ai[2] == 0f)
				{
					NPC.ai[2] = 1f;
					NPC.velocity.X = (float)(NPC.direction * 2);
				}
				return;
			}
			NPC.alpha = 0;
			if (NPC.ai[2] == 1f)
			{
				NPC.chaseable = true;
				NPC.noGravity = true;
				if (NPC.direction == 0)
				{
					NPC.TargetClosest(true);
				}
				if (NPC.wet)
				{
					bool flag14 = false;
					NPC.TargetClosest(false);
					if (!Main.player[NPC.target].dead)
					{
						flag14 = true;
					}
					if (!flag14)
					{
						if (NPC.collideX)
						{
							NPC.velocity.X = NPC.velocity.X * -1f;
							NPC.direction *= -1;
							NPC.netUpdate = true;
						}
						if (NPC.collideY)
						{
							NPC.netUpdate = true;
							if (NPC.velocity.Y > 0f)
							{
								NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -1f;
								NPC.directionY = -1;
								NPC.ai[0] = -1f;
							}
							else if (NPC.velocity.Y < 0f)
							{
								NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
								NPC.directionY = 1;
								NPC.ai[0] = 1f;
							}
						}
					}
					if (flag14)
					{
						NPC.TargetClosest(true);
						NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
						NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.1f;
						if (NPC.velocity.X > 2f)
						{
							NPC.velocity.X = 2f;
						}
						if (NPC.velocity.X < -2f)
						{
							NPC.velocity.X = -2f;
						}
						if (NPC.velocity.Y > 1f)
						{
							NPC.velocity.Y = 1f;
						}
						if (NPC.velocity.Y < -1f)
						{
							NPC.velocity.Y = -1f;
						}
						if ((Main.player[NPC.target].Center - NPC.Center).Length() < 350f)
						{
							NPC.localAI[0] += 1f;
							if (Main.netMode != 1 && NPC.localAI[0] >= 180f)
							{
								NPC.localAI[0] = 0f;
								if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
								{
									float speed = 4f;
									Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
									float num6 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X + (float)Main.rand.Next(-20, 21);
									float num7 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector.Y + (float)Main.rand.Next(-20, 21);
									float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
									num8 = speed / num8;
									num6 *= num8;
									num7 *= num8;
									int damage = 25;
									if (Main.expertMode)
									{
										damage = 19;
									}
									int beam = Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X + (NPC.spriteDirection == 1 ? 10f : -10f), NPC.Center.Y, num6, num7, Mod.Find<ModProjectile>("SulphuricAcidMist").Type, damage, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.1f;
						if (NPC.velocity.X < -1f || NPC.velocity.X > 1f)
						{
							NPC.velocity.X = NPC.velocity.X * 0.95f;
						}
						if (NPC.ai[0] == -1f)
						{
							NPC.velocity.Y = NPC.velocity.Y - 0.01f;
							if ((double)NPC.velocity.Y < -0.3)
							{
								NPC.ai[0] = 1f;
							}
						}
						else
						{
							NPC.velocity.Y = NPC.velocity.Y + 0.01f;
							if ((double)NPC.velocity.Y > 0.3)
							{
								NPC.ai[0] = -1f;
							}
						}
					}
					int num258 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
					int num259 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
					if (Main.tile[num258, num259 - 1].LiquidAmount > 128)
					{
						if (Main.tile[num258, num259 + 1].HasTile)
						{
							NPC.ai[0] = -1f;
						}
						else if (Main.tile[num258, num259 + 2].HasTile)
						{
							NPC.ai[0] = -1f;
						}
					}
					if ((double)NPC.velocity.Y > 0.4 || (double)NPC.velocity.Y < -0.4)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.95f;
					}
				}
				else
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.94f;
						if ((double)NPC.velocity.X > -0.2 && (double)NPC.velocity.X < 0.2)
						{
							NPC.velocity.X = 0f;
						}
					}
					NPC.velocity.Y = NPC.velocity.Y + 0.2f;
					if (NPC.velocity.Y > 5f)
					{
						NPC.velocity.Y = 5f;
					}
					NPC.ai[0] = 1f;
				}
				NPC.rotation = NPC.velocity.Y * (float)NPC.direction * 0.1f;
				if ((double)NPC.rotation < -0.2)
				{
					NPC.rotation = -0.2f;
				}
				if ((double)NPC.rotation > 0.2)
				{
					NPC.rotation = 0.2f;
					return;
				}
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Venom, 120, true);
		}

		public override void FindFrame(int frameHeight)
		{
			if (!NPC.wet)
			{
				NPC.frameCounter = 0.0;
				return;
			}
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe)
			{
				return 0f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur && spawnInfo.Water)
			{
				return 0.2f;
			}
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("CloakingGland").Type, 2));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}