using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer;
using Terraria.GameContent.Bestiary;
using Terraria.WorldBuilding;

namespace CalamityModClassicPreTrailer.NPCs.TheDevourerofGods
{
	[AutoloadBossHead]
	public class DevourerofGodsHead : ModNPC
	{
		private bool tail = false;
		private const int minLength = 100;
		private const int maxLength = 101;
		private bool halfLife = false;
		private bool halfLife2 = false;
		private int spawnDoGCountdown = 0;
		private float phaseSwitch = 0f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Devourer of Gods");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.8f,
				PortraitScale = 0.8f,
				CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/TheDevourerofGods/DevourerofGods_Bestiary",
				PortraitPositionXOverride = 40f,
				PortraitPositionYOverride = 40f
			};
			value.Position.X += 50f;
			value.Position.Y += 35f;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
				new FlavorTextBestiaryInfoElement("He has reached the status of an icon recognized far and wide, and while there could be some improvements made, there's no denying that he will forever be in the collective hearts and minds of all that know him.")
			});
		}

		public override void SetDefaults()
		{
			NPC.damage = 250; //150
			NPC.npcSlots = 5f;
			NPC.width = 64; //324
			NPC.height = 76; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 500000 : 450000; //1000000 960000
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 850000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.takenDamageMultiplier = 1.25f;
			NPC.aiStyle = 6; //new
			AIType = -1; //new
			AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.4f;
			NPC.boss = true;
			NPC.value = Item.buyPrice(0, 75, 0, 0);
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/ScourgeofTheUniverse");
			if (Main.expertMode)
			{
				NPC.scale = 1.5f;
			}
		}

		public override void AI()
		{
			CalamityGlobalNPC.DoGHead = NPC.whoAmI;
			float playerRunAcceleration = Main.player[NPC.target].velocity.Y == 0f ? Math.Abs(Main.player[NPC.target].moveSpeed * 0.3f) : (Main.player[NPC.target].runAcceleration * 0.8f);
			if (playerRunAcceleration <= 1f)
			{
				playerRunAcceleration = 1f;
			}
			if (Main.raining)
			{
				Main.raining = false;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			Vector2 vector = NPC.Center;
			bool flies = NPC.ai[2] == 0f;
			bool expertMode = Main.expertMode;
			bool speedBoost1 = (double)NPC.life <= (double)NPC.lifeMax * 0.8; //speed increase
			bool speedBoost2 = (double)NPC.life <= (double)NPC.lifeMax * 0.6; //speed increase
			bool speedBoost3 = (double)NPC.life <= (double)NPC.lifeMax * 0.4; //speed increase
			bool speedBoost4 = (double)NPC.life <= (double)NPC.lifeMax * 0.2; //speed increase
			bool speedBoost5 = (double)NPC.life <= (double)NPC.lifeMax * 0.1; //speed increase
			if (speedBoost4)
			{
				if (!halfLife)
				{
					if (CalamityWorldPreTrailer.revenge)
					{
						spawnDoGCountdown = 10;
					}
					string key = "Don't get cocky, kid!";
					Color messageColor = Color.Cyan;
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
				if (spawnDoGCountdown > 0)
				{
					spawnDoGCountdown--;
					if (spawnDoGCountdown == 0 && Main.netMode != 1)
					{
						for (int i = 0; i < 2; i++)
						{
							NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("DevourerofGodsHead2").Type);
						}
					}
				}
			}
			else if (speedBoost2)
			{
				if (!halfLife2)
				{
					if (CalamityWorldPreTrailer.revenge)
					{
						spawnDoGCountdown = 10;
					}
					halfLife2 = true;
				}
				if (spawnDoGCountdown > 0)
				{
					spawnDoGCountdown--;
					if (spawnDoGCountdown == 0 && Main.netMode != 1)
					{
						NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("DevourerofGodsHead2").Type);
					}
				}
			}
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			if (NPC.alpha != 0)
			{
				for (int spawnDust = 0; spawnDust < 2; spawnDust++)
				{
					int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			NPC.alpha -= 12;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (Main.netMode != 1)
			{
				if (!tail && NPC.ai[0] == 0f)
				{
					int Previous = NPC.whoAmI;
					for (int segmentSpawn = 0; segmentSpawn < maxLength; segmentSpawn++)
					{
						int segment = 0;
						if (segmentSpawn >= 0 && segmentSpawn < minLength)
						{
							segment = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsBody").Type, NPC.whoAmI);
						}
						else
						{
							segment = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsTail").Type, NPC.whoAmI);
						}
						Main.npc[segment].realLife = NPC.whoAmI;
						Main.npc[segment].ai[2] = (float)NPC.whoAmI;
						Main.npc[segment].ai[1] = (float)Previous;
						Main.npc[Previous].ai[0] = (float)segment;
						NPC.netUpdate = true;
						Previous = segment;
					}
					tail = true;
				}
				if (!NPC.active && Main.netMode == 2)
				{
					NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
				}
			}
			if (Main.player[NPC.target].dead)
			{
				flies = false;
				NPC.velocity.Y = NPC.velocity.Y + 2f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 2f;
				}
				if ((double)NPC.position.Y > Main.rockLayer * 16.0)
				{
					for (int a = 0; a < 200; a++)
					{
						if (Main.npc[a].aiStyle == NPC.aiStyle)
						{
							Main.npc[a].active = false;
						}
					}
				}
			}
			int num180 = (int)(NPC.position.X / 16f) - 1;
			int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
			int num182 = (int)(NPC.position.Y / 16f) - 1;
			int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
			if (num180 < 0)
			{
				num180 = 0;
			}
			if (num181 > Main.maxTilesX)
			{
				num181 = Main.maxTilesX;
			}
			if (num182 < 0)
			{
				num182 = 0;
			}
			if (num183 > Main.maxTilesY)
			{
				num183 = Main.maxTilesY;
			}
			if (NPC.ai[2] == 0f)
			{
				if (Main.netMode != 2)
				{
					if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, vector) < 5600f)
					{
						Main.player[Main.myPlayer].AddBuff(Mod.Find<ModBuff>("Warped").Type, 2);
					}
				}
				phaseSwitch += 1f;
				NPC.localAI[1] = 0f;
				float speed = playerRunAcceleration * 15f;
				float turnSpeed = playerRunAcceleration * 0.3f;
				float homingSpeed = playerRunAcceleration * 18f;
				float homingTurnSpeed = playerRunAcceleration * 0.33f;
				if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 5600f) //RAGE
				{
					phaseSwitch += 9f;
				}
				else if ((expertMode && speedBoost5) || CalamityWorldPreTrailer.death)
				{
					homingSpeed = playerRunAcceleration * 25f;
					homingTurnSpeed = playerRunAcceleration * 0.52f;
				}
				else if (speedBoost4)
				{
					homingSpeed = playerRunAcceleration * 23f;
					homingTurnSpeed = playerRunAcceleration * 0.47f;
				}
				else if (speedBoost3)
				{
					homingSpeed = playerRunAcceleration * 21.5f;
					homingTurnSpeed = playerRunAcceleration * 0.43f;
				}
				else if (speedBoost2)
				{
					homingSpeed = playerRunAcceleration * 20.5f;
					homingTurnSpeed = playerRunAcceleration * 0.39f;
				}
				else if (speedBoost1)
				{
					homingSpeed = playerRunAcceleration * 19f;
					homingTurnSpeed = playerRunAcceleration * 0.36f;
				}
				float num188 = speed;
				float num189 = turnSpeed;
				Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
				float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
				int num42 = -1;
				int num43 = (int)(Main.player[NPC.target].Center.X / 16f);
				int num44 = (int)(Main.player[NPC.target].Center.Y / 16f);
				for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
				{
					for (int num46 = num44; num46 <= num44 + 15; num46++)
					{
						if (WorldGen.SolidTile2(num45, num46))
						{
							num42 = num46;
							break;
						}
					}
					if (num42 > 0)
					{
						break;
					}
				}
				if (num42 > 0)
				{
					num42 *= 16;
					float num47 = (float)(num42 - 800);
					if (Main.player[NPC.target].position.Y > num47)
					{
						num192 = num47;
						if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 500f)
						{
							if (NPC.velocity.X > 0f)
							{
								num191 = Main.player[NPC.target].Center.X + 600f;
							}
							else
							{
								num191 = Main.player[NPC.target].Center.X - 600f;
							}
						}
					}
				}
				else
				{
					num188 = homingSpeed;
					num189 = homingTurnSpeed;
				}
				float num48 = num188 * 1.3f;
				float num49 = num188 * 0.7f;
				float num50 = NPC.velocity.Length();
				if (num50 > 0f)
				{
					if (num50 > num48)
					{
						NPC.velocity.Normalize();
						NPC.velocity *= num48;
					}
					else if (num50 < num49)
					{
						NPC.velocity.Normalize();
						NPC.velocity *= num49;
					}
				}
				if (num42 > 0)
				{
					for (int num51 = 0; num51 < 200; num51++)
					{
						if (Main.npc[num51].active && Main.npc[num51].type == NPC.type && num51 != NPC.whoAmI)
						{
							Vector2 vector3 = Main.npc[num51].Center - NPC.Center;
							if (vector3.Length() < 400f)
							{
								vector3.Normalize();
								vector3 *= 1000f;
								num191 -= vector3.X;
								num192 -= vector3.Y;
							}
						}
					}
				}
				else
				{
					for (int num52 = 0; num52 < 200; num52++)
					{
						if (Main.npc[num52].active && Main.npc[num52].type == NPC.type && num52 != NPC.whoAmI)
						{
							Vector2 vector4 = Main.npc[num52].Center - NPC.Center;
							if (vector4.Length() < 60f)
							{
								vector4.Normalize();
								vector4 *= 200f;
								num191 -= vector4.X;
								num192 -= vector4.Y;
							}
						}
					}
				}
				num191 = (float)((int)(num191 / 16f) * 16);
				num192 = (float)((int)(num192 / 16f) * 16);
				vector18.X = (float)((int)(vector18.X / 16f) * 16);
				vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
				num191 -= vector18.X;
				num192 -= vector18.Y;
				float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
				{
					try
					{
						vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
						num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
					}
					catch
					{
					}
					NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
					num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
					int num194 = NPC.width;
					num193 = (num193 - (float)num194) / num193;
					num191 *= num193;
					num192 *= num193;
					NPC.velocity = Vector2.Zero;
					NPC.position.X = NPC.position.X + num191;
					NPC.position.Y = NPC.position.Y + num192;
				}
				else
				{
					num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
					float num196 = System.Math.Abs(num191);
					float num197 = System.Math.Abs(num192);
					float num198 = num188 / num193;
					num191 *= num198;
					num192 *= num198;
					if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
					{
						if (NPC.velocity.X < num191)
						{
							NPC.velocity.X = NPC.velocity.X + num189;
						}
						else
						{
							if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - num189;
							}
						}
						if (NPC.velocity.Y < num192)
						{
							NPC.velocity.Y = NPC.velocity.Y + num189;
						}
						else
						{
							if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - num189;
							}
						}
						if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
						{
							if (NPC.velocity.Y > 0f)
							{
								NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
							}
							else
							{
								NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
							}
						}
						if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
						{
							if (NPC.velocity.X > 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num189 * 2f; //changed from 2
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X - num189 * 2f; //changed from 2
							}
						}
					}
					else
					{
						if (num196 > num197)
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + num189 * 1.1f; //changed from 1.1
							}
							else if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - num189 * 1.1f; //changed from 1.1
							}
							if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
							{
								if (NPC.velocity.Y > 0f)
								{
									NPC.velocity.Y = NPC.velocity.Y + num189;
								}
								else
								{
									NPC.velocity.Y = NPC.velocity.Y - num189;
								}
							}
						}
						else
						{
							if (NPC.velocity.Y < num192)
							{
								NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
							}
							else if (NPC.velocity.Y > num192)
							{
								NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
							}
							if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
							{
								if (NPC.velocity.X > 0f)
								{
									NPC.velocity.X = NPC.velocity.X + num189;
								}
								else
								{
									NPC.velocity.X = NPC.velocity.X - num189;
								}
							}
						}
					}
				}
				NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				if (phaseSwitch > 900f)
				{
					NPC.ai[2] = 1f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
			}
			else if (NPC.ai[2] == 1f)
			{
				if (Main.netMode != 2)
				{
					if (!Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active && Vector2.Distance(Main.player[Main.myPlayer].Center, vector) < 5600f)
					{
						Main.player[Main.myPlayer].AddBuff(Mod.Find<ModBuff>("ExtremeGrav").Type, 2);
					}
				}
				phaseSwitch += 1f;
				float speed = playerRunAcceleration * 19f;
				float turnSpeed = playerRunAcceleration * 0.28f;
				if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 5600f) //RAGE
				{
					speed = playerRunAcceleration * 80f;
					turnSpeed = playerRunAcceleration * 1f;
				}
				else if ((expertMode && speedBoost5) || CalamityWorldPreTrailer.death)
				{
					speed = playerRunAcceleration * 26f;
					turnSpeed = playerRunAcceleration * 0.45f;
				}
				else if (speedBoost4)
				{
					speed = playerRunAcceleration * 24.5f;
					turnSpeed = playerRunAcceleration * 0.4f;
				}
				else if (speedBoost3)
				{
					speed = playerRunAcceleration * 23f;
					turnSpeed = playerRunAcceleration * 0.36f;
				}
				else if (speedBoost2)
				{
					speed = playerRunAcceleration * 21.5f;
					turnSpeed = playerRunAcceleration * 0.33f;
				}
				else if (speedBoost1)
				{
					speed = playerRunAcceleration * 20f;
					turnSpeed = playerRunAcceleration * 0.3f;
				}
				if (!flies)
				{
					for (int num952 = num180; num952 < num181; num952++)
					{
						for (int num953 = num182; num953 < num183; num953++)
						{
							if (Main.tile[num952, num953] != null && ((Main.tile[num952, num953].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num952, num953].TileType] || (Main.tileSolidTop[(int)Main.tile[num952, num953].TileType] && Main.tile[num952, num953].TileFrameY == 0))) || Main.tile[num952, num953].LiquidAmount > 64))
							{
								Vector2 vector105;
								vector105.X = (float)(num952 * 16);
								vector105.Y = (float)(num953 * 16);
								if (NPC.position.X + (float)NPC.width > vector105.X && NPC.position.X < vector105.X + 16f && NPC.position.Y + (float)NPC.height > vector105.Y && NPC.position.Y < vector105.Y + 16f)
								{
									flies = true;
									break;
								}
							}
						}
					}
				}
				if (!flies)
				{
					NPC.localAI[1] = 1f;
					Rectangle rectangle12 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
					int num954 = 1000;
					bool flag95 = true;
					if (NPC.position.Y > Main.player[NPC.target].position.Y)
					{
						for (int num955 = 0; num955 < 255; num955++)
						{
							if (Main.player[num955].active)
							{
								Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - num954, (int)Main.player[num955].position.Y - num954, num954 * 2, num954 * 2);
								if (rectangle12.Intersects(rectangle13))
								{
									flag95 = false;
									break;
								}
							}
						}
						if (flag95)
						{
							flies = true;
						}
					}
				}
				else
				{
					NPC.localAI[1] = 0f;
				}
				float num188 = speed;
				float num189 = turnSpeed;
				Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
				float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
				num191 = (float)((int)(num191 / 16f) * 16);
				num192 = (float)((int)(num192 / 16f) * 16);
				vector18.X = (float)((int)(vector18.X / 16f) * 16);
				vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
				num191 -= vector18.X;
				num192 -= vector18.Y;
				float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
				{
					try
					{
						vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
						num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
					}
					catch
					{
					}
					NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
					num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
					int num194 = NPC.width;
					num193 = (num193 - (float)num194) / num193;
					num191 *= num193;
					num192 *= num193;
					NPC.velocity = Vector2.Zero;
					NPC.position.X = NPC.position.X + num191;
					NPC.position.Y = NPC.position.Y + num192;
				}
				else
				{
					if (!flies)
					{
						NPC.TargetClosest(true);
						NPC.velocity.Y = NPC.velocity.Y + turnSpeed; //turnspeed * 0.5f
						if (NPC.velocity.Y > num188) //speed
						{
							NPC.velocity.Y = num188;
						}
						if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.4)
						{
							if (NPC.velocity.X < 0f)
							{
								NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
							}
						}
						else if (NPC.velocity.Y == num188)
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + num189;
							}
							else if (NPC.velocity.X > num191)
							{
								NPC.velocity.X = NPC.velocity.X - num189;
							}
						}
						else if (NPC.velocity.Y > 4f)
						{
							if (NPC.velocity.X < 0f)
							{
								NPC.velocity.X = NPC.velocity.X + num189 * 0.9f;
							}
							else
							{
								NPC.velocity.X = NPC.velocity.X - num189 * 0.9f;
							}
						}
					}
					else
					{
						if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
						{
							float num195 = num193 / 40f;
							if (num195 < 10f)
							{
								num195 = 10f;
							}
							if (num195 > 20f)
							{
								num195 = 20f;
							}
							NPC.soundDelay = (int)num195;
							SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
						}
						num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
						float num196 = System.Math.Abs(num191);
						float num197 = System.Math.Abs(num192);
						float num198 = num188 / num193;
						num191 *= num198;
						num192 *= num198;
						if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
						{
							if (NPC.velocity.X < num191)
							{
								NPC.velocity.X = NPC.velocity.X + num189;
							}
							else
							{
								if (NPC.velocity.X > num191)
								{
									NPC.velocity.X = NPC.velocity.X - num189;
								}
							}
							if (NPC.velocity.Y < num192)
							{
								NPC.velocity.Y = NPC.velocity.Y + num189;
							}
							else
							{
								if (NPC.velocity.Y > num192)
								{
									NPC.velocity.Y = NPC.velocity.Y - num189;
								}
							}
							if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
							{
								if (NPC.velocity.Y > 0f)
								{
									NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
								}
								else
								{
									NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
								}
							}
							if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
							{
								if (NPC.velocity.X > 0f)
								{
									NPC.velocity.X = NPC.velocity.X + num189 * 2f;
								}
								else
								{
									NPC.velocity.X = NPC.velocity.X - num189 * 2f;
								}
							}
						}
						else
						{
							if (num196 > num197)
							{
								if (NPC.velocity.X < num191)
								{
									NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
								}
								else if (NPC.velocity.X > num191)
								{
									NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
								}
								if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
								{
									if (NPC.velocity.Y > 0f)
									{
										NPC.velocity.Y = NPC.velocity.Y + num189;
									}
									else
									{
										NPC.velocity.Y = NPC.velocity.Y - num189;
									}
								}
							}
							else
							{
								if (NPC.velocity.Y < num192)
								{
									NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
								}
								else if (NPC.velocity.Y > num192)
								{
									NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
								}
								if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
								{
									if (NPC.velocity.X > 0f)
									{
										NPC.velocity.X = NPC.velocity.X + num189;
									}
									else
									{
										NPC.velocity.X = NPC.velocity.X - num189;
									}
								}
							}
						}
					}
				}
				NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				if (flies)
				{
					if (NPC.localAI[0] != 1f)
					{
						NPC.netUpdate = true;
					}
					NPC.localAI[0] = 1f;
				}
				else
				{
					if (NPC.localAI[0] != 0f)
					{
						NPC.netUpdate = true;
					}
					NPC.localAI[0] = 0f;
				}
				if (phaseSwitch > 900f)
				{
					NPC.ai[2] = 0f;
					phaseSwitch = 0f;
					NPC.netUpdate = true;
					return;
				}
				if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
				{
					NPC.netUpdate = true;
					return;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.None;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.type == Mod.Find<ModProjectile>("SulphuricAcidMist2").Type || projectile.type == Mod.Find<ModProjectile>("EidolicWail").Type)
			{
				projectile.damage /= 4;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 2)
			{
				string key = "You think...you can butcher...ME!?";
				Color messageColor = Color.Cyan;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue(key), messageColor);
				}
				else if (Main.netMode == 2)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
				}
				modifiers.SetMaxDamage(0);
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool CheckActive()
		{
			return false;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity, Mod.Find<ModGore>("DoGHead").Type, 1f);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 15; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 30; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 300, true);
			if (CalamityWorldPreTrailer.death)
			{
				target.KillMe(PlayerDeathReason.ByOther(10), 1000.0, 0, false);
			}
			int num = Main.rand.Next(5);
			string key = "A fatal mistake!";
			if (num == 0)
			{
				key = "A fatal mistake!";
			}
			else if (num == 1)
			{
				key = "Good luck recovering from that!";
			}
			else if (num == 2)
			{
				key = "Delicious...";
			}
			else if (num == 3)
			{
				key = "Did that hurt?";
			}
			else if (num == 4)
			{
				key = "Nothing personal, kid.";
			}
			Color messageColor = Color.Cyan;
			if (Main.netMode == 0)
			{
				Main.NewText(Language.GetTextValue(key), messageColor);
			}
			else if (Main.netMode == 2)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
			}
			target.AddBuff(BuffID.Frostburn, 300, true);
			target.AddBuff(BuffID.Darkness, 300, true);
		}
	}
}