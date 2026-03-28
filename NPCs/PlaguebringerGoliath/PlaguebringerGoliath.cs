using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.PlaguebringerGoliath;
using CalamityModClassicPreTrailer.Items.Weapons.Plaguebringer;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace CalamityModClassicPreTrailer.NPCs.PlaguebringerGoliath
{
	[AutoloadBossHead]
	public class PlaguebringerGoliath : ModNPC
	{
		private const int MissileProjectiles = 5;
		private const float MissileAngleSpread = 90;
		private int MissileCountdown = 0;
		private bool halfLife = false;
		private int despawnTimer = 600;
		private bool canDespawn = false;
		private float chargeDistance = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Plaguebringer Goliath");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.4f,
				PortraitScale = 0.5f,
				PortraitPositionXOverride = 56f,
				PortraitPositionYOverride = -6f,
				SpriteDirection = -1
			};
			value.Position.X -= 48f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("A queen bee that has fallen victim to the plague, now it carries out its directives as its strongest soldier.")
			});
		}


		public override void SetDefaults()
		{
			NPC.damage = 100; //150
			NPC.npcSlots = 64f;
			NPC.width = 198; //324
			NPC.height = 198; //216
			NPC.defense = 55;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 77275 : 58500;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 110000;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 4000000 : 3700000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 25, 0, 0);
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/PlaguebringerGoliath");
		}

		public override void AI()
		{
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.15f, 0.35f, 0.05f);
			if (!halfLife && ((double)NPC.life <= (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive))
			{
				string key = "PLAGUE NUKE BARRAGE ARMED, PREPARING FOR LAUNCH!!!";
				Color messageColor = Color.Lime;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				halfLife = true;
			}
			if (halfLife && MissileCountdown == 0)
			{
				MissileCountdown = 600;
			}
			if (MissileCountdown > 1)
			{
				MissileCountdown--;
			}
			int num1038 = 0;
			for (int num1039 = 0; num1039 < 255; num1039++)
			{
				if (Main.player[num1039].active && !Main.player[num1039].dead && (NPC.Center - Main.player[num1039].Center).Length() < 1000f)
				{
					num1038++;
				}
			}
			if (expertMode)
			{
				int num1040 = (int)(50f * (1f - (float)NPC.life / (float)NPC.lifeMax));
				NPC.defense = NPC.defDefense + num1040;
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			Vector2 distFromPlayer = Main.player[NPC.target].Center - NPC.Center;
			bool aboveGroundEnrage = ((double)Main.player[NPC.target].position.Y < Main.worldSurface * 16.0 ||
				Main.player[NPC.target].position.Y > (float)((Main.maxTilesY - 200) * 16)) && !CalamityWorldPreTrailer.bossRushActive;
			bool jungleEnrage = false;
			if (!Main.player[NPC.target].ZoneJungle && !CalamityWorldPreTrailer.bossRushActive)
			{
				jungleEnrage = true;
			}
			if (jungleEnrage || Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 5600f)
			{
				if (despawnTimer > 0)
				{
					despawnTimer--;
				}
			}
			else
			{
				despawnTimer = 600;
			}
			canDespawn = (despawnTimer <= 0);
			if (canDespawn)
			{
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			if (NPC.ai[0] == -1f)
			{
				if (Main.netMode != 1)
				{
					float num595 = NPC.ai[1];
					int num596;
					do
					{
						num596 = Main.rand.Next(3);
						if (MissileCountdown == 1)
						{
							num596 = 4;
						}
						else if (num596 == 1)
						{
							num596 = 2;
						}
						else if (num596 == 2)
						{
							num596 = 3;
						}
					}
					while ((float)num596 == num595);
					if (num596 == 0 && ((double)NPC.life <= (double)NPC.lifeMax * 0.8 || CalamityWorldPreTrailer.death) && distFromPlayer.Length() < 1800f)
					{
						switch (Main.rand.Next(3))
						{
							case 0: chargeDistance = 0f; break;
							case 1: chargeDistance = 400f; break;
							case 2: chargeDistance = -400f; break;
						}
					}
					NPC.ai[0] = (float)num596;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.ai[3] = 0f;
					return;
				}
			}
			else if (NPC.ai[0] == 0f)
			{
				int num1043 = 2; //2
				if ((NPC.ai[1] > (float)(2 * num1043) && NPC.ai[1] % 2f == 0f) || distFromPlayer.Length() > 1800f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
				if (NPC.ai[1] % 2f == 0f)
				{
					NPC.TargetClosest(true);
					float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
					if (Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - chargeDistance)) < 20f)
					{
						switch (Main.rand.Next(3))
						{
							case 0: chargeDistance = 0f; break;
							case 1: chargeDistance = 400f; break;
							case 2: chargeDistance = -400f; break;
						}
						NPC.localAI[0] = 1f;
						NPC.ai[1] += 1f;
						NPC.ai[2] = 0f;
						float num1044 = revenge ? 24f : 22f; //16
						if (aboveGroundEnrage)
						{
							num1044 += 6f;
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
						{
							num1044 += 2f; //2 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
						{
							num1044 += 2f; //2 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
						{
							num1044 += 2f; //2 not a prob
						}
						if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
						{
							num1044 += 2f; //2 not a prob
						}
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							num1044 += 2f;
						}
						Vector2 vector117 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num1045 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector117.X;
						float num1046 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector117.Y;
						float num1047 = (float)Math.Sqrt((double)(num1045 * num1045 + num1046 * num1046));
						num1047 = num1044 / num1047;
						NPC.velocity.X = num1045 * num1047;
						NPC.velocity.Y = num1046 * num1047;
						NPC.direction = (playerLocation < 0 ? 1 : -1);
						NPC.spriteDirection = NPC.direction;
						SoundEngine.PlaySound(SoundID.Roar, NPC.position);
						return;
					}
					NPC.localAI[0] = 0f;
					float num1048 = revenge ? 10f : 9f; //12 not a prob
					float num1049 = revenge ? 0.15f : 0.13f; //0.15 not a prob
					if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
					{
						num1048 += 1f; //1 not a prob
						num1049 += 0.05f; //0.05 not a prob
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num1048 += 1f; //1 not a prob
						num1049 += 0.05f; //0.05 not a prob
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
					{
						num1048 += 2f; //2 not a prob
						num1049 += 0.05f; //0.05 not a prob
					}
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
					{
						num1048 += 2f; //2 not a prob
						num1049 += 0.1f; //0.1 not a prob
					}
					if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						num1048 += 2f;
						num1049 += 0.1f;
					}
					if (NPC.position.Y + (float)(NPC.height / 2) < (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - chargeDistance))
					{
						NPC.velocity.Y = NPC.velocity.Y + num1049;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - num1049;
					}
					if (NPC.velocity.Y < -12f)
					{
						NPC.velocity.Y = -num1048;
					}
					if (NPC.velocity.Y > 12f)
					{
						NPC.velocity.Y = num1048;
					}
					if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) > 600f)
					{
						NPC.velocity.X = NPC.velocity.X + 0.15f * (float)NPC.direction;
					}
					else if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) < 300f)
					{
						NPC.velocity.X = NPC.velocity.X - 0.15f * (float)NPC.direction;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X * 0.8f;
					}
					if (NPC.velocity.X < -16f)
					{
						NPC.velocity.X = -16f;
					}
					if (NPC.velocity.X > 16f)
					{
						NPC.velocity.X = 16f;
					}
					NPC.direction = (playerLocation < 0 ? 1 : -1);
					NPC.spriteDirection = NPC.direction;
					return;
				}
				else
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.direction = -1;
					}
					else
					{
						NPC.direction = 1;
					}
					NPC.spriteDirection = NPC.direction;
					int num1050 = 600; //600 not a prob
					if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
					{
						num1050 = 250;
					}
					else if (aboveGroundEnrage)
					{
						num1050 = 350;
					}
					else if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
					{
						num1050 = 400;
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
					{
						num1050 = revenge ? 425 : 450; //300 not a prob
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.25)
					{
						num1050 = revenge ? 450 : 475; //450 not a prob
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
					{
						num1050 = 500; //500 not a prob
					}
					else if ((double)NPC.life < (double)NPC.lifeMax * 0.75)
					{
						num1050 = 550; //550 not a prob
					}
					int num1051 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))
					{
						num1051 = -1;
					}
					if (NPC.direction == num1051 && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) > (float)num1050)
					{
						NPC.ai[2] = 1f;
					}
					if (NPC.ai[2] != 1f)
					{
						NPC.localAI[0] = 1f;
						return;
					}
					NPC.TargetClosest(true);
					NPC.spriteDirection = NPC.direction;
					NPC.localAI[0] = 0f;
					NPC.velocity *= 0.9f;
					float num1052 = revenge ? 0.13f : 0.115f; //0.1
					if (NPC.life < NPC.lifeMax / 2)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (NPC.life < NPC.lifeMax / 3)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (NPC.life < NPC.lifeMax / 5 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num1052)
					{
						NPC.ai[2] = 0f;
						NPC.ai[1] += 1f;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.TargetClosest(true);
				float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
				NPC.direction = (playerLocation < 0 ? 1 : -1);
				NPC.spriteDirection = NPC.direction;
				float num1053 = 12f;
				float num1054 = 0.2f;
				float num10542 = 0.1f;
				Vector2 vector118 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1055 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector118.X;
				float num1056 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 200f - vector118.Y;
				float num1057 = (float)Math.Sqrt((double)(num1055 * num1055 + num1056 * num1056));
				if (num1057 < 800f)
				{
					NPC.ai[0] = (((double)NPC.life <= (double)NPC.lifeMax * 0.66 || CalamityWorldPreTrailer.death) ? 5f : 1f);
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					return;
				}
				num1057 = num1053 / num1057;
				if (NPC.velocity.X < num1055)
				{
					NPC.velocity.X = NPC.velocity.X + num1054;
					if (NPC.velocity.X < 0f && num1055 > 0f)
					{
						NPC.velocity.X = NPC.velocity.X + num1054;
					}
				}
				else if (NPC.velocity.X > num1055)
				{
					NPC.velocity.X = NPC.velocity.X - num1054;
					if (NPC.velocity.X > 0f && num1055 < 0f)
					{
						NPC.velocity.X = NPC.velocity.X - num1054;
					}
				}
				if (NPC.velocity.Y < num1056)
				{
					NPC.velocity.Y = NPC.velocity.Y + num10542;
					if (NPC.velocity.Y < 0f && num1056 > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y + num10542;
						return;
					}
				}
				else if (NPC.velocity.Y > num1056)
				{
					NPC.velocity.Y = NPC.velocity.Y - num10542;
					if (NPC.velocity.Y > 0f && num1056 < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y - num10542;
						return;
					}
				}
			}
			else if (NPC.ai[0] == 1f)
			{
				NPC.localAI[0] = 0f;
				NPC.TargetClosest(true);
				Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
				float num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				if ((double)NPC.life < (double)NPC.lifeMax * 0.75 || CalamityWorldPreTrailer.bossRushActive)
				{
					NPC.ai[1] += 0.25f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
				{
					NPC.ai[1] += 0.25f;
				}
				bool flag103 = false;
				if (NPC.ai[1] > 40f)
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.NPCHit8, NPC.position);
					if (Main.netMode != 1)
					{
						int randomAmt = expertMode ? 2 : 4;
						int num1061;
						if (Main.rand.Next(randomAmt) == 0)
						{
							num1061 = Mod.Find<ModNPC>("PlagueBeeLargeG").Type;
						}
						else
						{
							num1061 = Mod.Find<ModNPC>("PlagueBeeG").Type;
						}
						if (expertMode && NPC.CountNPCS(Mod.Find<ModNPC>("PlagueMine").Type) < (aboveGroundEnrage ? 4 : 2))
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, Mod.Find<ModNPC>("PlagueMine").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						if (revenge && NPC.CountNPCS(Mod.Find<ModNPC>("PlaguebringerShade").Type) < (aboveGroundEnrage ? 2 : 1))
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, Mod.Find<ModNPC>("PlaguebringerShade").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						if (NPC.CountNPCS(Mod.Find<ModNPC>("PlagueBeeLargeG").Type) < 2)
						{
							int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, num1061, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.02f;
							Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.02f;
							Main.npc[num1062].localAI[0] = 60f;
							Main.npc[num1062].netUpdate = true;
						}
					}
				}
				if (num1060 > 800f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					float num1063 = 14f; //changed from 14 not a prob
					float num1064 = 0.2f; //changed from 0.1 not a prob
					float num10642 = 0.07f;
					vector120 = vector119;
					num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
					num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
					num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
					num1060 = num1063 / num1060;
					if (NPC.velocity.X < num1058)
					{
						NPC.velocity.X = NPC.velocity.X + num1064;
						if (NPC.velocity.X < 0f && num1058 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1064;
						}
					}
					else if (NPC.velocity.X > num1058)
					{
						NPC.velocity.X = NPC.velocity.X - num1064;
						if (NPC.velocity.X > 0f && num1058 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1064;
						}
					}
					if (NPC.velocity.Y < num1059)
					{
						NPC.velocity.Y = NPC.velocity.Y + num10642;
						if (NPC.velocity.Y < 0f && num1059 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num10642;
						}
					}
					else if (NPC.velocity.Y > num1059)
					{
						NPC.velocity.Y = NPC.velocity.Y - num10642;
						if (NPC.velocity.Y > 0f && num1059 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num10642;
						}
					}
				}
				else
				{
					NPC.velocity *= 0.9f;
				}
				float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
				NPC.direction = (playerLocation < 0 ? 1 : -1);
				NPC.spriteDirection = NPC.direction;
				if (NPC.ai[2] > 3f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 5f)
			{
				NPC.localAI[0] = 0f;
				NPC.TargetClosest(true);
				Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
				float num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				bool flag103 = false;
				if ((double)NPC.life < (double)NPC.lifeMax * 0.25 || CalamityWorldPreTrailer.bossRushActive)
				{
					NPC.ai[1] += 0.25f; //0.25 not a prob
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
				{
					NPC.ai[1] += 0.25f; //0.25 not a prob
				}
				if (NPC.ai[1] > 40f) //changed from 40 not a prob
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.Item88, NPC.position);
					if (Main.netMode != 1)
					{
						if (expertMode && NPC.CountNPCS(Mod.Find<ModNPC>("PlagueMine").Type) < (aboveGroundEnrage ? 6 : 4))
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, Mod.Find<ModNPC>("PlagueMine").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						if (revenge && NPC.CountNPCS(Mod.Find<ModNPC>("PlaguebringerShade").Type) < 1 && aboveGroundEnrage)
						{
							NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, Mod.Find<ModNPC>("PlaguebringerShade").Type, 0, 0f, 0f, 0f, 0f, 255);
						}
						float projectileSpeed = revenge ? 6f : 5f;
						float num1071 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector119.X + (float)Main.rand.Next(-80, 81);
						float num1072 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector119.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = projectileSpeed / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						if (NPC.CountNPCS(Mod.Find<ModNPC>("PlagueHomingMissile").Type) < (aboveGroundEnrage ? 8 : 5))
						{
							int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y, Mod.Find<ModNPC>("PlagueHomingMissile").Type, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[num1062].velocity.X = num1071;
							Main.npc[num1062].velocity.Y = num1072;
							Main.npc[num1062].netUpdate = true;
						}
					}
				}
				if (num1060 > 800f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					float num1063 = 14f; //changed from 14 not a prob
					float num1064 = 0.2f; //changed from 0.1 not a prob
					float num10642 = 0.07f;
					vector120 = vector119;
					num1058 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector120.X;
					num1059 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector120.Y;
					num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
					num1060 = num1063 / num1060;
					if (NPC.velocity.X < num1058)
					{
						NPC.velocity.X = NPC.velocity.X + num1064;
						if (NPC.velocity.X < 0f && num1058 > 0f)
						{
							NPC.velocity.X = NPC.velocity.X + num1064;
						}
					}
					else if (NPC.velocity.X > num1058)
					{
						NPC.velocity.X = NPC.velocity.X - num1064;
						if (NPC.velocity.X > 0f && num1058 < 0f)
						{
							NPC.velocity.X = NPC.velocity.X - num1064;
						}
					}
					if (NPC.velocity.Y < num1059)
					{
						NPC.velocity.Y = NPC.velocity.Y + num10642;
						if (NPC.velocity.Y < 0f && num1059 > 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y + num10642;
						}
					}
					else if (NPC.velocity.Y > num1059)
					{
						NPC.velocity.Y = NPC.velocity.Y - num10642;
						if (NPC.velocity.Y > 0f && num1059 < 0f)
						{
							NPC.velocity.Y = NPC.velocity.Y - num10642;
						}
					}
				}
				else
				{
					NPC.velocity *= 0.9f;
				}
				float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
				NPC.direction = (playerLocation < 0 ? 1 : -1);
				NPC.spriteDirection = NPC.direction;
				if (NPC.ai[2] > 3f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 1f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					if (NPC.ai[1] % 10f == 9f)
					{
						flag104 = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive || aboveGroundEnrage)
				{
					if (NPC.ai[1] % 20f == 19f)
					{
						flag104 = true;
					}
				}
				else if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
				{
					if (NPC.ai[1] % 25f == 24f)
					{
						flag104 = true;
					}
				}
				else if (NPC.ai[1] % 30f == 29f)
				{
					flag104 = true;
				}
				if (flag104 && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y && Collision.CanHit(vector121, 1, 1, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					SoundEngine.PlaySound(SoundID.Item42, NPC.position);
					if (Main.netMode != 1)
					{
						float projectileSpeed = revenge ? 6.5f : 6f;
						if (jungleEnrage || NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							projectileSpeed += 10f;
						}
						float num1071 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = projectileSpeed / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = 40; //projectile damage
						int num1075 = (Main.rand.Next(2) == 0 ? Mod.Find<ModProjectile>("PlagueStingerGoliath").Type : Mod.Find<ModProjectile>("PlagueStingerGoliathV2").Type);
						if (expertMode)
						{
							num1074 = 28; //112
							int damageBoost = (int)(6f * (1f - (float)NPC.life / (float)NPC.lifeMax));
							num1074 += damageBoost; //112 to 136
							if (Main.rand.Next(6) == 0)
							{
								num1074 += 8; //144 to 168
								num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
							}
						}
						else
						{
							if (Main.rand.Next(9) == 0)
							{
								num1074 = 50;
								num1075 = Mod.Find<ModProjectile>("HiveBombGoliath").Type;
							}
						}
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, -1f, 0f);
					}
				}
				if (NPC.position.Y > Main.player[NPC.target].position.Y - 200f) //200
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.98f;
					}
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
					if (NPC.velocity.Y > 2f)
					{
						NPC.velocity.Y = 2f;
					}
				}
				else if (NPC.position.Y < Main.player[NPC.target].position.Y - 500f) //500
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.98f;
					}
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
					if (NPC.velocity.Y < -2f)
					{
						NPC.velocity.Y = -2f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) > Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + 100f)
				{
					if (NPC.velocity.X > 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
					NPC.velocity.X = NPC.velocity.X - 0.1f;
					if (NPC.velocity.X > 8f)
					{
						NPC.velocity.X = 8f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - 100f)
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X = NPC.velocity.X * 0.98f;
					}
					NPC.velocity.X = NPC.velocity.X + 0.1f;
					if (NPC.velocity.X < -8f)
					{
						NPC.velocity.X = -8f;
					}
				}
				float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
				NPC.direction = (playerLocation < 0 ? 1 : -1);
				NPC.spriteDirection = NPC.direction;
				if (NPC.ai[1] > 300f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 3f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 4f)
			{
				int num1043 = 2; //2
				if (NPC.ai[1] > (float)(2 * num1043) && NPC.ai[1] % 2f == 0f)
				{
					MissileCountdown = 0;
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
				if (NPC.ai[1] % 2f == 0f)
				{
					NPC.TargetClosest(true);
					float playerLocation = NPC.Center.X - Main.player[NPC.target].Center.X;
					if (Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 500f)) < 20f)
					{
						if (MissileCountdown == 1)
						{
							SoundEngine.PlaySound(SoundID.Item116, NPC.position);
							if (Main.netMode != 1)
							{
								int speed = revenge ? 6 : 5;
								float spawnX = Main.rand.Next(1000) - 500 + NPC.Center.X;
								float spawnY = NPC.Center.Y;
								Vector2 baseSpawn = new Vector2(spawnX, spawnY);
								Vector2 baseVelocity = Main.player[NPC.target].Center - baseSpawn;
								baseVelocity.Normalize();
								baseVelocity = baseVelocity * speed;
								int damage = expertMode ? 42 : 57;
								for (int i = 0; i < MissileProjectiles; i++)
								{
									Vector2 spawn = baseSpawn;
									spawn.X = spawn.X + i * 30 - (MissileProjectiles * 15);
									Vector2 velocity = baseVelocity;
									velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-MissileAngleSpread / 2 + (MissileAngleSpread * i / (float)MissileProjectiles)));
									velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
									Projectile.NewProjectile(NPC.GetSource_FromThis(null), spawn.X, spawn.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("HiveBombGoliath").Type, damage, 10f, Main.myPlayer, 0f, Main.player[NPC.target].position.Y);
								}
							}
						}
						NPC.localAI[0] = 1f;
						NPC.ai[1] += 1f;
						NPC.ai[2] = 0f;
						float num1044 = revenge ? 28f : 26f; //16
						Vector2 vector117 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num1045 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector117.X;
						float num1046 = (Main.player[NPC.target].position.Y - 500f) + (float)(Main.player[NPC.target].height / 2) - vector117.Y;
						float num1047 = (float)Math.Sqrt((double)(num1045 * num1045 + num1046 * num1046));
						num1047 = num1044 / num1047;
						NPC.velocity.X = num1045 * num1047;
						NPC.velocity.Y = num1046 * num1047;
						NPC.direction = (playerLocation < 0 ? 1 : -1);
						NPC.spriteDirection = NPC.direction;
						return;
					}
					NPC.localAI[0] = 0f;
					float num1048 = 12f; //12 not a prob
					float num1049 = 0.15f; //0.15 not a prob
					if (NPC.position.Y + (float)(NPC.height / 2) < (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - 500f))
					{
						NPC.velocity.Y = NPC.velocity.Y + num1049;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y - num1049;
					}
					if (NPC.velocity.Y < -12f)
					{
						NPC.velocity.Y = -num1048;
					}
					if (NPC.velocity.Y > 12f)
					{
						NPC.velocity.Y = num1048;
					}
					if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) > 600f)
					{
						NPC.velocity.X = NPC.velocity.X + 0.15f * (float)NPC.direction;
					}
					else if (Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) < 300f)
					{
						NPC.velocity.X = NPC.velocity.X - 0.15f * (float)NPC.direction;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X * 0.8f;
					}
					if (NPC.velocity.X < -16f)
					{
						NPC.velocity.X = -16f;
					}
					if (NPC.velocity.X > 16f)
					{
						NPC.velocity.X = 16f;
					}
					NPC.direction = (playerLocation < 0 ? 1 : -1);
					NPC.spriteDirection = NPC.direction;
					return;
				}
				else
				{
					if (NPC.velocity.X < 0f)
					{
						NPC.direction = -1;
					}
					else
					{
						NPC.direction = 1;
					}
					NPC.spriteDirection = NPC.direction;
					int num1050 = 600; //600 not a prob
					int num1051 = 1;
					if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))
					{
						num1051 = -1;
					}
					if (NPC.direction == num1051 && Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2))) > (float)num1050)
					{
						NPC.ai[2] = 1f;
					}
					if (NPC.ai[2] != 1f)
					{
						NPC.localAI[0] = 1f;
						return;
					}
					NPC.TargetClosest(true);
					NPC.spriteDirection = NPC.direction;
					NPC.localAI[0] = 0f;
					NPC.velocity *= 0.9f;
					float num1052 = revenge ? 0.13f : 0.115f; //0.1
					if (NPC.life < NPC.lifeMax / 2)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (NPC.life < NPC.lifeMax / 3)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (NPC.life < NPC.lifeMax / 5 || CalamityWorldPreTrailer.bossRushActive)
					{
						NPC.velocity *= 0.9f;
						num1052 += 0.05f; //0.05
					}
					if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num1052)
					{
						NPC.ai[2] = 0f;
						NPC.ai[1] += 1f;
						return;
					}
				}
			}
		}

		public override bool CheckActive()
		{
			return canDespawn;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 2; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Pbg").Type, 2f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Pbg2").Type, 2f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Pbg3").Type, 2f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Pbg4").Type, 2f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Pbg5").Type, 2f);
				}
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 100;
				NPC.height = 100;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 46, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 46, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 46, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			if (NPC.localAI[0] == 1f)
			{
				texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/PlaguebringerGoliath/PlaguebringerGoliathChargeTex").Value;
			}
			else
			{
				if (NPC.localAI[1] == 0f)
				{
					texture = TextureAssets.Npc[NPC.type].Value;
				}
				else
				{
					texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/PlaguebringerGoliath/PlaguebringerGoliathAltTex").Value;
				}
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Microsoft.Xna.Framework.Color color24 = NPC.GetAlpha(drawColor);
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			int num156 = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
			int y3 = num156 * (int)NPC.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num157 = 8;
			int num158 = 2;
			int num159 = 1;
			float num160 = 0f;
			int num161 = num159;
			while (((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)) && Lighting.NotRetro)
			{
				Microsoft.Xna.Framework.Color color26 = NPC.GetAlpha(color25);
				{
					goto IL_6899;
				}
			IL_6881:
				num161 += num158;
				continue;
			IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				color26 *= num164 / ((float)NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f);
				Vector2 value4 = (NPC.oldPos[num161]);
				float num165 = NPC.rotation;
				Main.spriteBatch.Draw(texture, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, spriteEffects, 0f);
				goto IL_6881;
			}
			var something = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, something, 0);
			return false;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 4.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 4)
			{
				NPC.frame.Y = 0;
				if (NPC.localAI[0] != 1f)
				{
					NPC.localAI[1] += 1f;
				}
			}
			if (NPC.localAI[1] > 1f)
			{
				NPC.localAI[1] = 0f;
				NPC.netUpdate = true;
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<PlaguebringerGoliathTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<PlaguebringerGoliathBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PlaguebringerGoliathBag>()));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<PlagueCellCluster>(), 1, 10, 15));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<BloomStone>(), 10));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<MepheticSprayer>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Malevolence>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<VirulentKatana>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<DiseasedPike>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<PestilentDefiler>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<ThePlaguebringer>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<PlaguebringerGoliathMask>(), 7));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<TheHive>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<PlagueStaff>(), 4));
			npcLoot.Add(notExpert);
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 180);
			}
		}
	}
}