using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point2.Tiles;
using CalamityModClassic1Point2;
using Terraria.WorldBuilding;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.TheDevourerofGods
{
	[AutoloadBossHead]
	public class DevourerofGodsHead : ModNPC
	{
		public bool tail = false;
		public int minLength = CalamityWorld1Point2.revenge ? 60 : 100;
		public int maxLength = CalamityWorld1Point2.revenge ? 61 : 101;
		public bool halfLife = false;
		public float beamPortal = 0f;
		public float phaseSwitch = 0f;
		internal int dpsCap = CalamityWorld1Point2.downedDoG ? 190000 : 24000; //40
		private int damageTotal = 0;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Devourer of Gods");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Scale = 0.8f,
                PortraitScale = 0.8f,
                CustomTexturePath = "CalamityModClassic1Point2/NPCs/TheDevourerofGods/Bestiary",
                PortraitPositionXOverride = 40,
                PortraitPositionYOverride = 40
            };
            value.Position.X += 50;
            value.Position.Y += 35;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public override void SetDefaults()
		{
			NPC.damage = 300; //150
			NPC.npcSlots = 5f;
			NPC.width = 64; //324
			NPC.height = 76; //216
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorld1Point2.revenge ? 800000 : 750000;
			if (NPC.CountNPCS(Mod.Find<ModNPC>("DevourerofGodsHead").Type) > 0)
			{
				NPC.lifeMax = 550000;
			}
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.4f;
			NPC.boss = true;
			NPC.value = Item.buyPrice(5, 0, 0, 0);
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
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/ScourgeofTheUniverse");
			//bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("DevourerofGodsBag").Type;
			if (Main.expertMode)
			{
				NPC.scale = 1.5f;
			}
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("A fairly polished cosimc serpent. It is showing fair potential to be something special.")

            });
        }

        public override void AI()
		{
			bool enrage = false;
			int bossAlive = Mod.Find<ModNPC>("DevourerofGodsHead").Type;
			if (NPC.CountNPCS(bossAlive) < 2)
			{
				enrage = true;
			}
			float playerRunAcceleration = 1f; // Main.player[NPC.target].velocity.Y == 0f ? Math.Abs(Main.player[NPC.target].moveSpeed * 0.3f) : (Main.player[NPC.target].runAcceleration * 0.8f); I wish I could keep this authentic, but I'm not gonna bother dealing with reverse enginerring the changes to player movement speed code, so bye!
			if (playerRunAcceleration <= 1f)
			{
				playerRunAcceleration = 1f;
			}
			Vector2 vector = NPC.Center;
			NPC.takenDamageMultiplier = enrage ? 1.5f : 2f;
			bool flies = NPC.ai[2] == 0f;
			bool expertMode = Main.expertMode;
			bool speedBoost1 = (double)NPC.life <= (double)NPC.lifeMax * 0.8; //speed increase
			bool speedBoost2 = (double)NPC.life <= (double)NPC.lifeMax * 0.6; //speed increase
			bool speedBoost3 = (double)NPC.life <= (double)NPC.lifeMax * 0.4; //speed increase
			bool speedBoost4 = (double)NPC.life <= (double)NPC.lifeMax * 0.2; //speed increase
			bool speedBoost5 = (double)NPC.life <= (double)NPC.lifeMax * 0.1; //speed increase
			damageTotal -= dpsCap;
			if (damageTotal < 0)
			{
				damageTotal = 0;
			}
			if ((NPC.life <= NPC.lifeMax * 0.5f))
			{
				if (!halfLife)
				{
					if (CalamityWorld1Point2.revenge && NPC.CountNPCS(bossAlive) < 2)
					{
						NPC.SpawnOnPlayer(NPC.FindClosestPlayer(), Mod.Find<ModNPC>("DevourerofGodsHead").Type);
					}
					string key = "Don't get cocky, kid!";
					Color messageColor = Color.Cyan;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
					halfLife = true;
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
					int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TheDestroyer, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			NPC.alpha -= 12;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient)
            {
	            if (!tail && NPC.ai[0] == 0f)
				{
	            	int Previous = NPC.whoAmI;
					for (int segmentSpawn = 0; segmentSpawn < maxLength; segmentSpawn++)
	                {
	                    int segment = 0;
	                    if (segmentSpawn >= 0 && segmentSpawn < minLength)
	                    {
	                        segment = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsBody").Type, NPC.whoAmI);
	                    }
	                    else
	                    {
	                        segment = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("DevourerofGodsTail").Type, NPC.whoAmI);
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
                if (!NPC.active && Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
				}
                if (enrage)
                {
	                beamPortal += expertMode ? 2f : 1f;
					if (beamPortal >= 1800f)
					{
						beamPortal = 0f;
						NPC.TargetClosest(true);
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							float projectileSpeed = 5f;
							Vector2 shootFromVector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
							float playerPositionX = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - shootFromVector.X + (float)Main.rand.Next(-20, 21);
							float playerPositionY = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - shootFromVector.Y + (float)Main.rand.Next(-20, 21);
							float playerPosition = (float)Math.Sqrt((double)(playerPositionX * playerPositionX + playerPositionY * playerPositionY));
							playerPosition = projectileSpeed / playerPosition;
							playerPositionX *= playerPosition;
							playerPositionY *= playerPosition;
							playerPositionX += (float)Main.rand.Next(-10, 11) * 0.05f;
							playerPositionY += (float)Main.rand.Next(-10, 11) * 0.05f;
							int projectileDamage = expertMode ? 150 : 180;
							int projectileType = Mod.Find<ModProjectile>("DoGBeamPortal").Type;
							shootFromVector.X += playerPositionX * 5f;
							shootFromVector.Y += playerPositionY * 5f;
							int projectileShot = Projectile.NewProjectile(NPC.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, playerPositionX, playerPositionY, projectileType, projectileDamage, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[projectileShot].timeLeft = 900;
							NPC.netUpdate = true;
						}
					}
                }
            }
			if (Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(false);
				flies = false;
				NPC.velocity.Y = NPC.velocity.Y + 10f;
				if ((double)NPC.position.Y > Main.worldSurface * 16.0)
				{
					NPC.velocity.Y = NPC.velocity.Y + 10f;
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
				if (Main.netMode != NetmodeID.Server)
				{
					if (!Main.player[NPC.target].dead && Main.player[NPC.target].active)
					{
						Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("Warped").Type, 2);
					}
				}
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
				phaseSwitch += 1f;
				NPC.localAI[1] = 0f;
				float speed = playerRunAcceleration * 20f;
				float turnSpeed = playerRunAcceleration * 0.5f;
				float homingSpeed = playerRunAcceleration * 25f;
				float homingTurnSpeed = playerRunAcceleration * 0.55f;
				if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 8400f) //RAGE
				{
					speed = playerRunAcceleration * 50f;
					turnSpeed = playerRunAcceleration * 2.5f;
					homingSpeed = playerRunAcceleration * 150f;
					homingTurnSpeed = playerRunAcceleration * 5f;
				}
				else if (expertMode && speedBoost5)
				{
					homingSpeed = playerRunAcceleration * 31.907f;
					homingTurnSpeed = playerRunAcceleration * 0.702f;
				}
				else if (speedBoost4)
				{
					homingSpeed = playerRunAcceleration * 30.388f;
					homingTurnSpeed = playerRunAcceleration * 0.669f;
				}
				else if (speedBoost3)
				{
					homingSpeed = playerRunAcceleration * 28.941f;
					homingTurnSpeed = playerRunAcceleration * 0.637f;
				}
				else if (speedBoost2)
				{
					homingSpeed = playerRunAcceleration * 27.563f;
					homingTurnSpeed = playerRunAcceleration * 0.607f;
				}
				else if (speedBoost1)
				{
					homingSpeed = playerRunAcceleration * 26.25f;
					homingTurnSpeed = playerRunAcceleration * 0.578f;
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
				if (Main.netMode != NetmodeID.Server)
				{
					if (!Main.player[NPC.target].dead && Main.player[NPC.target].active)
					{
						Main.player[NPC.target].AddBuff(Mod.Find<ModBuff>("ExtremeGrav").Type, 2);
					}
				}
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
				phaseSwitch += 1f;
				float speed = playerRunAcceleration * 35f;
				float turnSpeed = playerRunAcceleration * 0.55f;
				if (Vector2.Distance(Main.player[NPC.target].Center, vector) > 8400f) //RAGE
				{
					speed = playerRunAcceleration * 80f;
					turnSpeed = playerRunAcceleration * 1f;
				}
				else if (expertMode && speedBoost5)
				{
					speed = playerRunAcceleration * 44.67f;
					turnSpeed = playerRunAcceleration * 0.702f;
				}
				else if (speedBoost4)
				{
					speed = playerRunAcceleration * 42.543f;
					turnSpeed = playerRunAcceleration * 0.669f;
				}
				else if (speedBoost3)
				{
					speed = playerRunAcceleration * 40.517f;
					turnSpeed = playerRunAcceleration * 0.637f;
				}
				else if (speedBoost2)
				{
					speed = playerRunAcceleration * 38.588f;
					turnSpeed = playerRunAcceleration * 0.607f;
				}
				else if (speedBoost1)
				{
					speed = playerRunAcceleration * 36.75f;
					turnSpeed = playerRunAcceleration * 0.576f;
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
						NPC.velocity.Y = NPC.velocity.Y + (turnSpeed * 0.5f);
						if (NPC.velocity.Y > num188)
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
		
		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			ModifyHit(ref modifiers.FinalDamage.Base);
		}

		public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[NPC.target];
			if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
			{
				modifiers.FinalDamage /= 2;
				modifiers.DisableCrit();
			}
			if (projectile.penetrate == -1 && !projectile.minion)
			{
				projectile.penetrate = 1;
			}
			else if (projectile.penetrate >= 1)
			{
				projectile.penetrate = 1;
			}
			ModifyHit(ref modifiers.FinalDamage.Base);
		}
		
		public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			OnHit(hit.Damage);
		}
		
		private void ModifyHit(ref float damage)
		{
			if (damage > NPC.lifeMax / 8)
			{
				damage = NPC.lifeMax / 8;
			}
		}
		
		private void OnHit(float damage)
		{
			damageTotal += (int)damage * 60;
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				ModPacket netMessage = GetPacket(DoGMessageType.Damage);
				netMessage.Write(damage * 60);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					netMessage.Write(Main.myPlayer);
				}
				netMessage.Send();
			}
		}
		
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 2)
			{
				string key = "You think...you can butcher...ME!?";
				Color messageColor = Color.Cyan;
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(key, messageColor);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
				}
				modifiers.FinalDamage.Base = 1;
			}
			if (damageTotal >= dpsCap * 60)
			{
				modifiers.FinalDamage.Base = 0;
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
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DoGHead").Type, 1f);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 15; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 30; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
			if ((NPC.CountNPCS(Mod.Find<ModNPC>("StasisProbe").Type) + NPC.CountNPCS(Mod.Find<ModNPC>("StasisProbeNaked").Type)) < 3)
			{
				if (NPC.life > 0 && Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextBool(25))
				{
					int randomSpawn = Main.rand.Next(2);
					if (randomSpawn == 0)
					{
						randomSpawn = Mod.Find<ModNPC>("StasisProbe").Type;
					}
					else
					{
						randomSpawn = Mod.Find<ModNPC>("StasisProbeNaked").Type;
					}
					int num660 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), randomSpawn, 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == NetmodeID.Server && num660 < 200)
					{
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
					}
					NPC.netUpdate = true;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage *= 2;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 150, true);
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 600, true);
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
				key = "Nothing personal, kid";
			}
			Color messageColor = Color.Cyan;
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText((key), messageColor);
			}
			else if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
			}
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Frostburn, 400, true);
				target.AddBuff(BuffID.Darkness, 400, true);
			}
		}
		
		private ModPacket GetPacket(DoGMessageType type)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)CalamityModClassic1Point2MessageType.DoG);
			packet.Write(NPC.whoAmI);
			packet.Write((byte)type);
			return packet;
		}
		
		public void HandlePacket(BinaryReader reader)
		{
			DoGMessageType type = (DoGMessageType)reader.ReadByte();
			if (type == DoGMessageType.Damage)
			{
				int damage = reader.ReadInt32();
				damageTotal += damage;
				if (Main.netMode == NetmodeID.Server)
				{
					ModPacket netMessage = GetPacket(DoGMessageType.Damage);
					int ignore = reader.ReadInt32();
					netMessage.Write(damage);
					netMessage.Send(-1, ignore);
				}
			}
		}
	}
	
	enum DoGMessageType : byte
	{
		Damage
	}
}