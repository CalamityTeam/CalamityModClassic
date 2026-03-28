using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.HiveMind;
using CalamityModClassicPreTrailer.Items.Placeables;
using CalamityModClassicPreTrailer.Items.Weapons.HiveMind;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

//reminder to remove unnecessary "CalamityModClassicPreTrailer." before CalamityWorld
//reminder to replace "ModLoader.GetMod("CalamityModClassicPreTrailer")." with "mod."
//enable the //CalamityGlobalNPC.hiveMind2 bit

/* states:
 * 0 = slow drift
 * 1 = reelback and teleport after spawn enemy
 * 2 = reelback for spin lunge + death legacy
 * 3 = spin lunge
 * 4 = semicircle spawn arc
 * 5 = raindash
 * 6 = deceleration
 */

namespace CalamityModClassicPreTrailer.NPCs.HiveMind
{
	[AutoloadBossHead]
	public class HiveMindP2 : ModNPC
	{
		//this block of values can be modified in SetDefaults() based on difficulty mode or something
		int minimumDriftTime = 300;
		int teleportRadius = 300;
		int decelerationTime = 30;
		int reelbackFade = 3;       //divide 255 by this for duration of reelback in ticks
		float arcTime = 45f;        //ticks needed to complete movement for spawn and rain attacks (DEATH ONLY)
		float driftSpeed = 2f;      //default speed when slowly floating at player
		float driftBoost = 1f;      //max speed added as health decreases
		int lungeDelay = 90;        //# of ticks long hive mind spends sliding to a stop before lunging
		int lungeTime = 30;
		int lungeFade = 15;         //divide 255 by this for duration of hive mind spin before slowing for lunge
		double lungeRots = 0.2;     //number of revolutions made while spinning/fading in for lunge
		bool dashStarted = false;
		int phase2timer = 360;
		int rotationDirection;
		double rotation;
		double rotationIncrement;
		int state = 0;
		int previousState = 0;
		int nextState = 0;
		int reelCount = 0;
		int oldDamage = 40;
		Vector2 deceleration;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Hive Mind");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				new FlavorTextBestiaryInfoElement("The deity and master of the corruption, it serves as the corruption's greatest offensive asset.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 5f;
			NPC.damage = 40;
			NPC.width = 150; //324
			NPC.height = 120; //216
			NPC.defense = 5;
			NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 7560 : 5800;
			if (CalamityWorldPreTrailer.death)
			{
				NPC.lifeMax = 12500;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				NPC.lifeMax = CalamityWorldPreTrailer.death ? 1600000 : 1400000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = -1; //new
			AIType = -1; //new
			NPC.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 6, 0, 0);
			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/HiveMind");
			NPCID.Sets.TrailCacheLength[NPC.type] = 8;
			NPCID.Sets.TrailingMode[NPC.type] = 1;
			if (Main.expertMode)
			{
				minimumDriftTime = 120;
				reelbackFade = 5;
			}
			if (CalamityWorldPreTrailer.revenge)
			{
				lungeRots = 0.3;
				minimumDriftTime = 90;
				reelbackFade = 6;
				lungeTime = 25;
				driftSpeed = 3f;
				driftBoost = 2f;
			}
			if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
			{
				lungeRots = 0.4;
				minimumDriftTime = 60;
				reelbackFade = 7;
				lungeTime = 20;
				driftSpeed = 4f;
				driftBoost = 1f;
			}
			phase2timer = minimumDriftTime;
			rotationIncrement = 0.0246399424 * lungeRots * lungeFade;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(state);
			writer.Write(nextState);
			writer.Write(phase2timer);
			writer.Write(dashStarted);
			writer.Write(rotationDirection);
			writer.Write(rotation);
			writer.Write(previousState);
			writer.Write(reelCount);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			state = reader.ReadInt32();
			nextState = reader.ReadInt32();
			phase2timer = reader.ReadInt32();
			dashStarted = reader.ReadBoolean();
			rotationDirection = reader.ReadInt32();
			rotation = reader.ReadDouble();
			previousState = reader.ReadInt32();
			reelCount = reader.ReadInt32();
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (!NPC.active || NPC.IsABestiaryIconDummy)
			{
				return true;
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			Microsoft.Xna.Framework.Color color24 = drawColor;
			color24 = NPC.GetAlpha(color24);
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			Texture2D texture2D3 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/HiveMind/HiveMindP2").Value;
			int num156 = TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type];
			int y3 = num156 * (int)NPC.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture2D3.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num157 = 8;
			int num158 = 2;
			int num159 = 1;
			float num160 = 0f;
			int num161 = num159;
			while (state != 0 && Lighting.NotRetro && ((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)))
			{
				Microsoft.Xna.Framework.Color color26 = color25;
				color26 = NPC.GetAlpha(color26);
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
				SpriteEffects effects = spriteEffects;
				Main.spriteBatch.Draw(texture2D3, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, NPC.scale, effects, 0f);
				goto IL_6881;
			}
			var something = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color24, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, something, 0);
			return false;
		}

		private void SpawnStuff()
		{
			Player player = Main.player[NPC.target];
			for (int i = 0; i < 5; i++)
			{
				bool spawnedSomething = false;
				int type = NPCID.EaterofSouls;
				int maxAmount = 0;
				int random = !CalamityWorldPreTrailer.death && Collision.CanHit(NPC.Center, 1, 1, player.position, player.width, player.height) ? 5 : 3;
				switch (Main.rand.Next(random))
				{
					case 0:
						type = NPCID.DevourerHead;
						maxAmount = 1;
						break;
					case 1:
						type = Mod.Find<ModNPC>("DankCreeper").Type;
						maxAmount = 1;
						break;
					case 2:
						type = Mod.Find<ModNPC>("DankCreeper").Type;
						maxAmount = 2;
						break;
					case 3:
						type = Mod.Find<ModNPC>("HiveBlob2").Type;
						maxAmount = 2;
						break;
					case 4:
						type = NPCID.EaterofSouls;
						maxAmount = 2;
						break;
					case 5:
						type = Mod.Find<ModNPC>("DarkHeart").Type;
						maxAmount = 2;
						break;
				}
				int numToSpawn = maxAmount - NPC.CountNPCS(type);
				while (numToSpawn > 0)
				{
					numToSpawn--;
					spawnedSomething = true;
					int spawn = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + Main.rand.Next(NPC.width), (int)NPC.position.Y + Main.rand.Next(NPC.height), type);
					Main.npc[spawn].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
					Main.npc[spawn].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
				}
				if (spawnedSomething)
					return;
			}
		}

		private void ReelBack()
		{
			NPC.alpha = 0;
			phase2timer = 0;
			deceleration = NPC.velocity / 255f * reelbackFade;
			if (CalamityWorldPreTrailer.death)
			{
				state = 2;
				SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);
			}
			else
			{
				if (Main.netMode != 1)
					SpawnStuff();
				state = nextState;
				nextState = 0;
				if (state == 2)
				{
					SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
				}
				else
				{
					SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);
				}
			}
		}

		public override void AI()
		{
			Player player = Main.player[NPC.target];
			NPC.defense = (player.ZoneCorrupt || CalamityWorldPreTrailer.bossRushActive) ? 5 : 9999;
			CalamityGlobalNPC.hiveMind2 = NPC.whoAmI;
			if (NPC.alpha != 0)
			{
				if (NPC.damage != 0)
				{
					oldDamage = NPC.damage;
					NPC.damage = 0;
				}
			}
			else
			{
				NPC.damage = oldDamage;
			}
			switch (state)
			{
				case 0: //slowdrift
					if (NPC.alpha > 0)
						NPC.alpha -= 3;
					if (nextState == 0)
					{
						NPC.TargetClosest(true);
						if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
						{
							do nextState = Main.rand.Next(3, 6);
							while (nextState == previousState);
							previousState = nextState;
						}
						else
						{
							if (CalamityWorldPreTrailer.revenge && (Main.rand.Next(4) == 0 || reelCount == 3))
							{
								reelCount = 0;
								nextState = 2;
							}
							else
							{
								reelCount++;
								nextState = 1;
								NPC.ai[1] = 0f;
								NPC.ai[2] = 0f;
							}
						}
						if (nextState == 3)
							rotation = MathHelper.ToRadians(Main.rand.Next(360));
						NPC.netUpdate = true;
					}
					if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f)
					{
						NPC.TargetClosest(true);
						if (NPC.timeLeft > 60)
							NPC.timeLeft = 60;
						if (NPC.localAI[3] < 120f)
						{
							float[] aiArray = NPC.localAI;
							int number = 3;
							float num244 = aiArray[number];
							aiArray[number] = num244 + 1f;
						}
						if (NPC.localAI[3] > 60f)
						{
							NPC.velocity.Y = NPC.velocity.Y + (NPC.localAI[3] - 60f) * 0.5f;
						}
						return;
					}
					if (NPC.localAI[3] > 0f)
					{
						float[] aiArray = NPC.localAI;
						int number = 3;
						float num244 = aiArray[number];
						aiArray[number] = num244 - 1f;
						return;
					}
					NPC.velocity = player.Center - NPC.Center;
					phase2timer--;
					if (phase2timer <= -180) //no stalling drift mode forever
					{
						NPC.velocity *= 2f / 255f * reelbackFade;
						ReelBack();
						NPC.netUpdate = true;
					}
					else
					{
						NPC.velocity.Normalize();
						if (Main.expertMode || CalamityWorldPreTrailer.bossRushActive) //variable velocity in expert and up
						{
							NPC.velocity *= driftSpeed + driftBoost * (NPC.lifeMax - NPC.life) / NPC.lifeMax;
						}
						else
						{
							NPC.velocity *= driftSpeed;
						}
					}
					break;
				case 1: //reelback and teleport
					NPC.alpha += reelbackFade;
					NPC.velocity -= deceleration;
					if (NPC.alpha >= 255)
					{
						NPC.alpha = 255;
						NPC.velocity = Vector2.Zero;
						state = 0;
						if (Main.netMode != 1 && NPC.ai[1] != 0f && NPC.ai[2] != 0f)
						{
							NPC.position.X = NPC.ai[1] * 16 - NPC.width / 2;
							NPC.position.Y = NPC.ai[2] * 16 - NPC.height / 2;
						}
						phase2timer = minimumDriftTime + Main.rand.Next(121);
						NPC.netUpdate = true;
					}
					else if (NPC.ai[1] == 0f && NPC.ai[2] == 0f)
					{
						for (int i = 0; i < 10; i++)
						{
							int posX = (int)player.Center.X / 16 + Main.rand.Next(15, 46) * (Main.rand.Next(2) == 0 ? -1 : 1);
							int posY = (int)player.Center.Y / 16 + Main.rand.Next(15, 46) * (Main.rand.Next(2) == 0 ? -1 : 1);
							if (!WorldGen.SolidTile(posX, posY) && Collision.CanHit(new Vector2(posX * 16, posY * 16), 1, 1, player.position, player.width, player.height))
							{
								NPC.ai[1] = posX;
								NPC.ai[2] = posY;
								NPC.netUpdate = true;
								break;
							}
						}
					}
					break;
				case 2: //reelback for lunge + death legacy
					NPC.alpha += reelbackFade;
					NPC.velocity -= deceleration;
					if (NPC.alpha >= 255)
					{
						NPC.alpha = 255;
						NPC.velocity = Vector2.Zero;
						dashStarted = false;
						if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
						{
							state = nextState;
							nextState = 0;
							previousState = state;
						}
						else
						{
							state = 3;
						}
						if (player.velocity.X > 0)
							rotationDirection = 1;
						else if (player.velocity.X < 0)
							rotationDirection = -1;
						else
							rotationDirection = player.direction;
					}
					break;
				case 3: //rev lunge
					NPC.netUpdate = true;
					if (NPC.alpha > 0)
					{
						NPC.alpha -= lungeFade;
						if (Main.netMode != 1)
						{
							NPC.Center = player.Center + new Vector2(teleportRadius, 0).RotatedBy(rotation);
						}
						rotation += rotationIncrement * rotationDirection;
						phase2timer = lungeDelay;
					}
					else
					{
						phase2timer--;
						if (!dashStarted)
						{
							if (phase2timer <= 0)
							{
								phase2timer = lungeTime;
								NPC.velocity = player.Center - NPC.Center;
								NPC.velocity.Normalize();
								NPC.velocity *= teleportRadius / lungeTime;
								dashStarted = true;
								SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
							}
							else
							{
								if (Main.netMode != 1)
								{
									NPC.Center = player.Center + new Vector2(teleportRadius, 0).RotatedBy(rotation);
								}
								rotation += rotationIncrement * rotationDirection * phase2timer / lungeDelay;
							}
						}
						else
						{
							if (phase2timer <= 0)
							{
								state = 6;
								phase2timer = 0;
								deceleration = NPC.velocity / decelerationTime;
							}
						}
					}
					break;
				case 4: //enemy spawn arc (death mode)
					if (NPC.alpha > 0)
					{
						NPC.alpha -= 5;
						if (Main.netMode != 1)
						{
							NPC.Center = player.Center;
							NPC.position.Y += teleportRadius;
						}
						NPC.netUpdate = true;
					}
					else
					{
						if (!dashStarted)
						{
							dashStarted = true;
							SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
							NPC.velocity.X = 3.14159265f * teleportRadius / arcTime;
							NPC.velocity *= rotationDirection;
							NPC.netUpdate = true;
						}
						else
						{
							NPC.velocity = NPC.velocity.RotatedBy(3.14159265 / arcTime * -rotationDirection);
							phase2timer++;
							if (phase2timer == (int)arcTime / 6)
							{
								phase2timer = 0;
								NPC.ai[0]++;
								if (Main.netMode != 1 && Collision.CanHit(NPC.Center, 1, 1, player.position, player.width, player.height)) //draw line of sight
								{
									if (NPC.ai[0] == 2 || NPC.ai[0] == 4)
									{
										if ((Main.expertMode || CalamityWorldPreTrailer.bossRushActive) && NPC.CountNPCS(Mod.Find<ModNPC>("DarkHeart").Type) < 2)
										{
											NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DarkHeart").Type);
										}
									}
									else if (NPC.CountNPCS(NPCID.EaterofSouls) < 2)
									{
										NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.EaterofSouls);
									}
								}
								if (NPC.ai[0] == 6)
								{
									NPC.velocity = NPC.velocity.RotatedBy(3.14159265 / arcTime * -rotationDirection);
									SpawnStuff();
									state = 6;
									NPC.ai[0] = 0;
									deceleration = NPC.velocity / decelerationTime;
								}
							}
						}
					}
					break;
				case 5: //raindash (death mode)
					if (NPC.alpha > 0)
					{
						NPC.alpha -= 5;
						if (Main.netMode != 1)
						{
							NPC.Center = player.Center;
							NPC.position.Y -= teleportRadius;
							NPC.position.X += teleportRadius * rotationDirection;
						}
						NPC.netUpdate = true;
					}
					else
					{
						if (!dashStarted)
						{
							dashStarted = true;
							SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
							NPC.velocity.X = teleportRadius / arcTime * 3;
							NPC.velocity *= -rotationDirection;
							NPC.netUpdate = true;
						}
						else
						{
							phase2timer++;
							if (phase2timer == (int)arcTime / 20)
							{
								phase2timer = 0;
								NPC.ai[0]++;
								if (Main.netMode != 1)
								{
									int damage = Main.expertMode ? 14 : 18;
									Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.position.X + Main.rand.Next(NPC.width), NPC.position.Y + Main.rand.Next(NPC.height), 0, 0, Mod.Find<ModProjectile>("ShadeNimbusHostile").Type, damage, 0, Main.myPlayer, 11, 0);
								}
								if (NPC.ai[0] == 10)
								{
									state = 6;
									NPC.ai[0] = 0;
									deceleration = NPC.velocity / decelerationTime;
								}
							}
						}
					}
					break;
				case 6: //deceleration
					NPC.velocity -= deceleration;
					phase2timer++;
					if (phase2timer == decelerationTime)
					{
						phase2timer = minimumDriftTime + Main.rand.Next(121);
						state = 0;
						NPC.netUpdate = true;
					}
					break;
			}
		}

		public override bool CanHitNPC(NPC target)/* tModPorter Suggestion: Return true instead of null */
		{
			if (NPC.alpha > 0)
				return false;
			return true;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return NPC.alpha <= 0; //no damage when not fully visible
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (phase2timer < 0 && modifiers.FinalDamage.Base > 1)
			{
				NPC.velocity *= -4f;
				ReelBack();
				NPC.netUpdate = true;
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < hit.Damage / NPC.lifeMax * 100.0; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (Main.netMode != 1 && Main.rand.Next(15) == 0 && NPC.CountNPCS(Mod.Find<ModNPC>("HiveBlob2").Type) < 2)
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_FromThis(null), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("HiveBlob2").Type);
			}
			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("HiveMindP2Gore").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("HiveMindP2Gore2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("HiveMindP2Gore3").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("HiveMindP2Gore4").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("HiveMindP2Gore5").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("HiveMindP2Gore6").Type, 1f);
				}
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 200;
				NPC.height = 150;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 14, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
			npcLoot.Add(new CommonDrop(ModContent.ItemType<HiveMindTrophy>(), 10));
			npcLoot.Add(ItemDropRule.ByCondition(new ArmageddonDropRuleCondition(),
				ModContent.ItemType<HiveMindBag>(),
				1,
				5, 5));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<HiveMindBag>()));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<HiveMindMask>(), 7));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<ShaderainStaff>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<LeechingDagger>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<ShadowdropStaff>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<PerfectDark>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<Shadethrower>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<RotBall>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<DankStaff>(), 4));
			notExpert.OnSuccess(new CommonDrop(ModContent.ItemType<TrueShadowScale>(), 1, 25, 31));
			notExpert.OnSuccess(new CommonDrop(ItemID.DemoniteBar, 1, 7, 11));
			notExpert.OnSuccess(new CommonDrop(ItemID.RottenChunk, 1, 9, 16));
			notExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.CursedFlame, 1, 10, 21));
			npcLoot.Add(notExpert);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}
	}
}
