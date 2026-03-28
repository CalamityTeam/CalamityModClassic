using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items.Leviathan;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	[AutoloadBossHead]
	public class Siren : ModNPC
	{
		public static bool phase2 = false;
		public static bool phase3 = false;
		private bool spawnedLevi = false;
		private bool secondClone = false;
		private float phaseSwitch = 0f;
		private float chargeSwitch = 0f;
		private float anotherFloat = 0f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Siren");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.9f,
				PortraitScale = 0.74f
			};
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("Once a resident of a now fallen floating city, she attracts unsuspecting prey with her song, to be devoured by her companion, the Leviathan.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 70; //150
			NPC.npcSlots = 16f;
			NPC.width = 100; //324
			NPC.height = 100; //216
			NPC.defense = 25;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 41600 : 27400;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 58650;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 2800000 : 2600000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 15, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("MarkedforDeath").Type] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[BuffID.Daybreak] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("DemonFlames").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Nightwither").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("Shred").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("WhisperingDeath").Type] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("SilvaStun").Type] = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Siren");
		}

		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 0.3f);
			Player player = Main.player[NPC.target];
			bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			bool playerWet = player.wet;
			float num998 = 8f;
			float scaleFactor3 = 300f;
			float num999 = 800f;
			float num1001 = 5f;
			float scaleFactor4 = 0.8f;
			int num1002 = 0;
			float scaleFactor5 = 10f;
			float num1003 = 30f;
			float num1004 = 150f;
			float num1006 = 0.333333343f;
			float num1007 = 8f;
			Vector2 vector = NPC.Center;
			Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
			bool isNotOcean = player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400));
			int npcType = Mod.Find<ModNPC>("Leviathan").Type;
			bool halfLife = (double)NPC.life <= (double)NPC.lifeMax * 0.5;
			bool leviAlive = NPC.AnyNPCs(npcType);
			float num1000 = leviAlive ? 14f : 16f;
			float num1005 = leviAlive ? 14f : 16f;
			float chargeSpeedDivisor = leviAlive ? 13.85f : 15.85f;
			num1006 *= num1005;
			if ((halfLife || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive) && Main.netMode != 1)
			{
				if (!spawnedLevi)
				{
					if (revenge)
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y - 200, Mod.Find<ModNPC>("SirenClone").Type);
					}
					Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/LeviathanAndSiren");
					NPC.SpawnOnPlayer(player.whoAmI, npcType);
					spawnedLevi = true;
				}
				phase2 = true;
			}
			int defenseMult = phase2 ? 2 : 3;
			if ((!leviAlive && halfLife) || CalamityWorldPreTrailer.death)
			{
				NPC.defense = 25 * defenseMult;
			}
			else
			{
				NPC.defense = 25;
			}
			if ((double)NPC.life <= (double)NPC.lifeMax * 0.25 && Main.netMode != 1)
			{
				if (!secondClone)
				{
					if (revenge)
					{
						NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y - 200, Mod.Find<ModNPC>("SirenClone").Type);
					}
					secondClone = true;
				}
				phase3 = true;
			}
			if (NPC.ai[3] == 0f && NPC.localAI[1] == 0f && Main.netMode != 1)
			{
				int num6 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("SirenIce").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.ai[3] = (float)(num6 + 1);
				NPC.localAI[1] = -1f;
				NPC.netUpdate = true;
				Main.npc[num6].ai[0] = (float)NPC.whoAmI;
				Main.npc[num6].netUpdate = true;
			}
			int num7 = (int)NPC.ai[3] - 1;
			if (num7 != -1 && Main.npc[num7].active && Main.npc[num7].type == Mod.Find<ModNPC>("SirenIce").Type)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = isNotOcean && !CalamityWorldPreTrailer.bossRushActive;
				NPC.ai[3] = 0f;
				if (NPC.localAI[1] == -1f)
				{
					NPC.localAI[1] = revenge ? 600f : 1200f;
				}
				if (NPC.localAI[1] > 0f)
				{
					NPC.localAI[1] -= 1f;
				}
			}
			if (isNotOcean)
			{
				NPC.alpha += 3;
				if (NPC.alpha >= 150)
				{
					NPC.alpha = 150;
				}
			}
			else
			{
				NPC.alpha -= 5;
				if (NPC.alpha <= 0)
				{
					NPC.alpha = 0;
				}
			}
			if (Main.rand.Next(300) == 0)
			{
				SoundEngine.PlaySound(SoundID.Zombie35, NPC.position);
			}
			int num1038 = 0;
			for (int num1039 = 0; num1039 < 255; num1039++)
			{
				if (Main.player[num1039].active && !Main.player[num1039].dead && (NPC.Center - Main.player[num1039].Center).Length() < 1000f)
				{
					num1038++;
				}
			}
			if (NPC.timeLeft < 3000)
			{
				NPC.timeLeft = 3000;
			}
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(true);
			}
			else if (NPC.ai[0] == -1f)
			{
				int random = ((double)NPC.life <= (double)NPC.lifeMax * 0.5) ? 3 : 2;
				int num871 = Main.rand.Next(random);
				if (num871 == 0)
				{
					num871 = 0;
				}
				else if (num871 == 1)
				{
					num871 = 2;
				}
				else
				{
					num871 = 3;
				}
				NPC.ai[0] = (float)num871;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				return;
			}
			else if (NPC.ai[0] == 0f)
			{
				NPC.TargetClosest(true);
				NPC.rotation = NPC.velocity.X * 0.02f;
				NPC.spriteDirection = NPC.direction;
				float num1053 = 9f;
				float num1054 = 0.15f;
				float num10542 = 0.05f;
				Vector2 vector118 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1055 = player.position.X + (float)(player.width / 2) - vector118.X;
				float num1056 = player.position.Y + (float)(player.height / 2) - 200f - vector118.Y;
				float num1057 = (float)Math.Sqrt((double)(num1055 * num1055 + num1056 * num1056));
				if (num1057 < 600f)
				{
					NPC.ai[0] = 1f;
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
				NPC.rotation = NPC.velocity.X * 0.02f;
				NPC.localAI[0] = 0f;
				NPC.TargetClosest(true);
				Vector2 vector119 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.8f);
				Vector2 vector120 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num1058 = player.position.X + (float)(player.width / 2) - vector120.X;
				float num1059 = player.position.Y + (float)(player.height / 2) - vector120.Y;
				float num1060 = (float)Math.Sqrt((double)(num1058 * num1058 + num1059 * num1059));
				NPC.ai[1] += 1f;
				NPC.ai[1] += (float)(num1038 / 2);
				if ((double)NPC.life < (double)NPC.lifeMax * 0.5 || CalamityWorldPreTrailer.bossRushActive)
				{
					NPC.ai[1] += 0.25f;
				}
				if ((double)NPC.life < (double)NPC.lifeMax * 0.1 || CalamityWorldPreTrailer.bossRushActive)
				{
					NPC.ai[1] += 0.25f;
				}
				bool flag103 = false;
				if (NPC.ai[1] > 20f)
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] += 1f;
					flag103 = true;
				}
				if (Collision.CanHit(vector119, 1, 1, player.position, player.width, player.height) && flag103)
				{
					SoundEngine.PlaySound(SoundID.Item85, NPC.position);
					if (Main.netMode != 1)
					{
						int num1061 = 371;
						int num1062 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)vector119.X, (int)vector119.Y - 30, num1061, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num1062].velocity.X = (float)Main.rand.Next(-200, 201) * 0.01f;
						Main.npc[num1062].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.01f;
						Main.npc[num1062].localAI[0] = 60f;
						Main.npc[num1062].netUpdate = true;
						Main.npc[num1062].damage = leviAlive ? 100 : 140;
					}
				}
				if (num1060 > 600f || !Collision.CanHit(new Vector2(vector119.X, vector119.Y - 30f), 1, 1, player.position, player.width, player.height))
				{
					float num1063 = 14f;
					float num1064 = 0.15f;
					float num10642 = 0.05f;
					vector120 = vector119;
					num1058 = player.position.X + (float)(player.width / 2) - vector120.X;
					num1059 = player.position.Y + (float)(player.height / 2) - vector120.Y;
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
				NPC.spriteDirection = NPC.direction;
				if (NPC.ai[2] > 4f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 2f)
			{
				NPC.rotation = NPC.velocity.X * 0.02f;
				Vector2 vector121 = new Vector2(NPC.position.X + (float)(NPC.width / 2) + (float)(Main.rand.Next(20) * NPC.direction), NPC.position.Y + (float)NPC.height * 0.4f);
				NPC.ai[1] += 1f;
				bool flag104 = false;
				if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
				{
					if (NPC.ai[1] % 10f == 9f)
					{
						flag104 = true;
					}
				}
				else if ((!leviAlive && halfLife) || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					if (NPC.ai[1] % 15f == 14f)
					{
						flag104 = true;
					}
				}
				else
				{
					if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
					{
						if (NPC.ai[1] % 20f == 19f)
						{
							flag104 = true;
						}
					}
					else if (NPC.life < NPC.lifeMax / 2)
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
				}
				if (flag104 && (NPC.position.Y + (float)NPC.height < player.position.Y || (!leviAlive && halfLife)) && Collision.CanHit(vector121, 1, 1, player.position, player.width, player.height))
				{
					if (Main.netMode != 1)
					{
						float num1070 = revenge ? 13f : 11f;
						if (NPC.GetGlobalNPC<CalamityGlobalNPC>().enraged || (Config.BossRushXerocCurse && CalamityWorldPreTrailer.bossRushActive))
						{
							num1070 = 24f;
						}
						else if (isNotOcean || (!leviAlive && halfLife) || CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
						{
							num1070 = revenge ? 19f : 18f;
						}
						else if (!playerWet)
						{
							num1070 = revenge ? 16f : 15f;
						}
						else
						{
							if ((double)NPC.life < (double)NPC.lifeMax * 0.1)
							{
								num1070 += 2f;
							}
						}
						float num1071 = player.position.X + (float)player.width * 0.5f - vector121.X + (float)Main.rand.Next(-80, 81);
						float num1072 = player.position.Y + (float)player.height * 0.5f - vector121.Y + (float)Main.rand.Next(-40, 41);
						float num1073 = (float)Math.Sqrt((double)(num1071 * num1071 + num1072 * num1072));
						num1073 = num1070 / num1073;
						num1071 *= num1073;
						num1072 *= num1073;
						int num1074 = expertMode ? 26 : 32;
						int num1075 = Mod.Find<ModProjectile>("WaterSpear").Type;
						switch (Main.rand.Next(6))
						{
							case 0: num1075 = Mod.Find<ModProjectile>("SirenSong").Type; break;
							case 1: num1075 = Mod.Find<ModProjectile>("FrostMist").Type; break;
							case 2:
							case 3:
							case 4:
							case 5: num1075 = Mod.Find<ModProjectile>("WaterSpear").Type; break;
						}
						if (isNotOcean)
						{
							num1074 *= 2;
						}
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector121.X, vector121.Y, num1071, num1072, num1075, num1074, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (NPC.position.Y > player.position.Y - 200f) //200
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.985f;
					}
					NPC.velocity.Y = NPC.velocity.Y - 0.1f;
					if (NPC.velocity.Y > 2f)
					{
						NPC.velocity.Y = 2f;
					}
				}
				else if (NPC.position.Y < player.position.Y - 500f) //500
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = NPC.velocity.Y * 0.985f;
					}
					NPC.velocity.Y = NPC.velocity.Y + 0.1f;
					if (NPC.velocity.Y < -2f)
					{
						NPC.velocity.Y = -2f;
					}
				}
				if (NPC.position.X + (float)(NPC.width / 2) > player.position.X + (float)(player.width / 2) + 100f)
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
				if (NPC.position.X + (float)(NPC.width / 2) < player.position.X + (float)(player.width / 2) - 100f)
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
				float playerLocation = NPC.Center.X - player.Center.X;
				NPC.direction = (playerLocation < 0 ? 1 : -1);
				NPC.spriteDirection = NPC.direction;
				if (NPC.ai[1] > 300f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[0] == 3f)
			{
				NPC.TargetClosest(false);
				NPC.rotation = NPC.velocity.ToRotation();
				if (Math.Sign(NPC.velocity.X) != 0)
				{
					NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
				}
				if (NPC.rotation < -1.57079637f)
				{
					NPC.rotation += 3.14159274f;
				}
				if (NPC.rotation > 1.57079637f)
				{
					NPC.rotation -= 3.14159274f;
				}
				NPC.spriteDirection = Math.Sign(NPC.velocity.X);
				phaseSwitch += 1f;
				if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
				{
					phaseSwitch += 1f;
				}
				if (chargeSwitch == 0f) //line up the charge
				{
					float scaleFactor6 = num998;
					Vector2 center4 = NPC.Center;
					Vector2 center5 = player.Center;
					Vector2 vector126 = center5 - center4;
					Vector2 vector127 = vector126 - Vector2.UnitY * scaleFactor3;
					float num1013 = vector126.Length();
					vector126 = Vector2.Normalize(vector126) * scaleFactor6;
					vector127 = Vector2.Normalize(vector127) * scaleFactor6;
					bool flag64 = Collision.CanHit(NPC.Center, 1, 1, player.Center, 1, 1);
					if (anotherFloat >= 120f)
					{
						flag64 = true;
					}
					float num1014 = 8f;
					flag64 = (flag64 && vector126.ToRotation() > 3.14159274f / num1014 && vector126.ToRotation() < 3.14159274f - 3.14159274f / num1014);
					if (num1013 > num999 || !flag64)
					{
						NPC.velocity.X = (NPC.velocity.X * (num1000 - 1f) + vector127.X) / chargeSpeedDivisor;
						NPC.velocity.Y = (NPC.velocity.Y * (num1000 - 1f) + vector127.Y) / chargeSpeedDivisor;
						if (!flag64)
						{
							anotherFloat += 1f;
							if (anotherFloat == 120f)
							{
								NPC.netUpdate = true;
							}
						}
						else
						{
							anotherFloat = 0f;
						}
					}
					else
					{
						chargeSwitch = 1f;
						NPC.ai[2] = vector126.X;
						anotherFloat = vector126.Y;
						NPC.netUpdate = true;
					}
				}
				else if (chargeSwitch == 1f) //pause before charging
				{
					NPC.velocity *= scaleFactor4;
					NPC.ai[1] += 1f;
					if (NPC.ai[1] >= num1001)
					{
						chargeSwitch = 2f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
						Vector2 velocity = new Vector2(NPC.ai[2], anotherFloat) + new Vector2((float)Main.rand.Next(-num1002, num1002 + 1), (float)Main.rand.Next(-num1002, num1002 + 1)) * 0.04f;
						velocity.Normalize();
						velocity *= scaleFactor5;
						NPC.velocity = velocity;
					}
				}
				else if (chargeSwitch == 2f) //charging
				{
					float num1016 = num1003;
					NPC.ai[1] += 1f;
					bool flag65 = Vector2.Distance(NPC.Center, player.Center) > num1004 && NPC.Center.Y > player.Center.Y;
					if ((NPC.ai[1] >= num1016 && flag65) || NPC.velocity.Length() < num1007)
					{
						chargeSwitch = 0f;
						NPC.ai[1] = 0f;
						NPC.ai[2] = 0f;
						anotherFloat = 0f;
						NPC.velocity /= 2f;
						NPC.netUpdate = true;
						NPC.ai[1] = 45f;
						chargeSwitch = 3f;
					}
					else
					{
						Vector2 center6 = NPC.Center;
						Vector2 center7 = player.Center;
						Vector2 vec2 = center7 - center6;
						vec2.Normalize();
						if (vec2.HasNaNs())
						{
							vec2 = new Vector2((float)NPC.direction, 0f);
						}
						NPC.velocity = (NPC.velocity * (num1005 - 1f) + vec2 * (NPC.velocity.Length() + num1006)) / num1005;
					}
				}
				else if (chargeSwitch == 3f) //slow down after charging and reset
				{
					NPC.ai[1] -= 1f;
					if (NPC.ai[1] <= 0f)
					{
						chargeSwitch = 0f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
					}
					NPC.velocity *= 0.98f;
				}
				if (phaseSwitch > 300f)
				{
					NPC.ai[0] = -1f;
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
					anotherFloat = 0f;
					chargeSwitch = 0f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			if (player.dead || Vector2.Distance(player.Center, vector) > 5600f)
			{
				if (NPC.localAI[3] < 120f)
				{
					NPC.localAI[3] += 1f;
				}
				if (NPC.localAI[3] > 60f)
				{
					NPC.velocity.Y = NPC.velocity.Y + (NPC.localAI[3] - 60f) * 0.25f;
					if ((double)NPC.position.Y > Main.rockLayer * 16.0)
					{
						for (int x = 0; x < 200; x++)
						{
							if (Main.npc[x].type == Mod.Find<ModNPC>("Leviathan").Type)
							{
								Main.npc[x].active = false;
								Main.npc[x].netUpdate = true;
							}
						}
						NPC.active = false;
						NPC.netUpdate = true;
					}
				}
				return;
			}
			if (NPC.localAI[3] > 0f)
			{
				NPC.localAI[3] -= 1f;
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Siren").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Siren2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Siren3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Siren4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Siren5").Type, 1f);
					for (int k = 0; k < 50; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) //2 total states (ice shield or no ice shield)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			if (NPC.dontTakeDamage)
			{
				switch ((int)NPC.localAI[2])
				{
					case 0:
						texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/SirenAlt").Value;
						break;
					case 1:
						texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/SirenAltSinging").Value;
						break;
					case 2:
						texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/SirenAltStabbing").Value;
						break;
				}
			}
			else
			{
				switch ((int)NPC.localAI[2])
				{
					case 0:
						texture = TextureAssets.Npc[NPC.type].Value;
						break;
					case 1:
						texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/SirenSinging").Value;
						break;
					case 2:
						texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Leviathan/SirenStabbing").Value;
						break;
				}
			}
			CalamityModClassicPreTrailer.DrawTexture(spriteBatch, texture, 0, NPC, drawColor);
			return false;
		}

		public override void FindFrame(int frameHeight) //6 total frames, 3 total texture types
		{
			if (NPC.ai[0] == 2f)
			{
				NPC.localAI[2] = 0f;
			}
			else if (NPC.ai[0] <= 1f)
			{
				NPC.localAI[2] = 1f;
			}
			else
			{
				NPC.localAI[2] = 2f;
			}
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 12.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 6)
			{
				NPC.frame.Y = 0;
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			LeadingConditionRule noLevi = new LeadingConditionRule(new NoLevi());
			npcLoot.Add(noLevi.OnSuccess(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<LeviathanBag>(),
				1,
				5, 5)));
			npcLoot.Add(noLevi.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<LeviathanBag>())));
			npcLoot.Add(noLevi.OnSuccess(new CommonDrop(Mod.Find<ModItem>("LeviathanTrophy").Type, 10)));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("EnchantedPearl").Type, 10)); //done
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.HotlineFishingHook, 10));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.BottomlessBucket, 10));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SuperAbsorbantSponge, 10));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.CratePotion, 5, 5, 9)); 
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.FishingPotion, 5, 5, 9));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SonarPotion, 5, 5, 9));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), Mod.Find<ModItem>("IOU").Type, 1));
			noLevi.OnSuccess(ItemDropRule.ByCondition(new Conditions.NotExpert(), Mod.Find<ModItem>("LeviathanMask").Type, 7)); 
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Atlantis").Type, 4)));
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("BrackishFlask").Type, 4)));
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Leviatitan").Type, 4)));
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("LureofEnthrallment").Type, 4)));
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("SirensSong").Type, 4)));
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Greentide").Type, 4)));
			noLevi.OnSuccess(notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("Atlantis").Type, 4)));
			npcLoot.Add(notExpert);
			npcLoot.Add(noLevi);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Wet, 120, true);
		}

		public override bool CheckActive()
		{
			return false;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
	}
}