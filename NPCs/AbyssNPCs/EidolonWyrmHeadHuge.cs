using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.Items;
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
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.WorldBuilding;
using Conditions = Terraria.GameContent.ItemDropRules.Conditions;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class EidolonWyrmHeadHuge : ModNPC
	{
		public bool detectsPlayer = false;
		public bool flies = true;
		public const int minLength = 40;
		public const int maxLength = 41;
		public float speed = 7.5f; //10
		public float turnSpeed = 0.15f; //0.15
		bool TailSpawned = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eidolon Wyrm");
			NPCID.Sets.BossBestiaryPriority.Add(Type);
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Scale = 0.50f,
				PortraitScale = 0.6f,
				PortraitPositionXOverride = 40,
				CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/AbyssNPCs/AdultEidolonWyrm_Bestiary"
			};
			value.Position.X += 55;
			value.Position.Y += 5;
			NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
				new FlavorTextBestiaryInfoElement("Colossal creatures lurking within the void of the deepest layer of the Abyss. May your soul be spared from encountering one of these.")
			});
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 24f;
			NPC.damage = 1500;
			NPC.width = 254; //36
			NPC.height = 138; //20
			NPC.defense = 3000;
			NPC.lifeMax = 1000000;
			NPC.aiStyle = -1;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(10, 0, 0, 0);
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.netAlways = true;
		}

		public override void AI()
		{
			if (NPC.justHit || (double)NPC.life <= (double)NPC.lifeMax * 0.98 || Main.player[NPC.target].chaosState)
			{
				detectsPlayer = true;
				NPC.damage = 1500;
			}
			else
			{
				NPC.damage = 0;
			}
			NPC.chaseable = detectsPlayer;
			if (detectsPlayer)
			{
				if (NPC.soundDelay <= 0)
				{
					NPC.soundDelay = 420;
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/EidolonWyrmRoarClose") { Volume = 2.5f }, NPC.position);
				}
			}
			else
			{
				if (Main.rand.Next(900) == 0)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/EidolonWyrmRoarClose"){ Volume = 2.5f }, NPC.position);
				}
			}
			if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			if (Main.netMode != 1)
			{
				if (!TailSpawned && NPC.ai[0] == 0f)
				{
					int Previous = NPC.whoAmI;
					for (int num36 = 0; num36 < maxLength; num36++)
					{
						int lol = 0;
						if (num36 >= 0 && num36 < minLength)
						{
							if (num36 % 2 == 0)
							{
								lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("EidolonWyrmBodyHuge").Type, NPC.whoAmI);
							}
							else
							{
								lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("EidolonWyrmBodyAltHuge").Type, NPC.whoAmI);
							}
						}
						else
						{
							lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("EidolonWyrmTailHuge").Type, NPC.whoAmI);
						}
						Main.npc[lol].realLife = NPC.whoAmI;
						Main.npc[lol].ai[2] = (float)NPC.whoAmI;
						Main.npc[lol].ai[1] = (float)Previous;
						Main.npc[Previous].ai[0] = (float)lol;
						NetMessage.SendData(23, -1, -1, null, lol, 0f, 0f, 0f, 0);
						Previous = lol;
					}
					TailSpawned = true;
				}
				if (detectsPlayer)
				{
					NPC.localAI[0] += 1f;
					if (NPC.localAI[0] >= 300f)
					{
						NPC.localAI[0] = 0f;
						NPC.TargetClosest(true);
						NPC.netUpdate = true;
						int damage = Main.expertMode ? 300 : 400;
						float xPos = (Main.rand.Next(2) == 0 ? NPC.position.X + 200f : NPC.position.X - 200f);
						Vector2 vector2 = new Vector2(xPos, NPC.position.Y + Main.rand.Next(-200, 201));
						int random = Main.rand.Next(3);
						if (random == 0)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), vector2.X, vector2.Y, 0f, 0f, 465, damage, 0f, Main.myPlayer, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(null), -vector2.X, -vector2.Y, 0f, 0f, 465, damage, 0f, Main.myPlayer, 0f, 0f);
						}
						else if (random == 1)
						{
							Vector2 vec = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center);
							vec = Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center + Main.player[NPC.target].velocity * 20f);
							if (vec.HasNaNs())
							{
								vec = new Vector2((float)NPC.direction, 0f);
							}
							for (int n = 0; n < 1; n++)
							{
								Vector2 vector4 = vec * 4f;
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), vector2.X, vector2.Y, vector4.X, vector4.Y, 464, damage, 0f, Main.myPlayer, 0f, 1f);
								Projectile.NewProjectile(NPC.GetSource_FromThis(null), -vector2.X, -vector2.Y, -vector4.X, -vector4.Y, 464, damage, 0f, Main.myPlayer, 0f, 1f);
							}
						}
						else
						{
							if (Math.Abs(Main.player[NPC.target].velocity.X) > 0.1f || Math.Abs(Main.player[NPC.target].velocity.Y) > 0.1f)
							{
								SoundEngine.PlaySound(SoundID.Item117, Main.player[NPC.target].position);
								for (int num621 = 0; num621 < 20; num621++)
								{
									int num622 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y),
										Main.player[NPC.target].width, Main.player[NPC.target].height, 185, 0f, 0f, 100, default(Color), 2f);
									Main.dust[num622].velocity *= 0.6f;
									if (Main.rand.Next(2) == 0)
									{
										Main.dust[num622].scale = 0.5f;
										Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
									}
								}
								for (int num623 = 0; num623 < 30; num623++)
								{
									int num624 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y),
										Main.player[NPC.target].width, Main.player[NPC.target].height, 185, 0f, 0f, 100, default(Color), 3f);
									Main.dust[num624].noGravity = true;
									num624 = Dust.NewDust(new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y),
										Main.player[NPC.target].width, Main.player[NPC.target].height, 185, 0f, 0f, 100, default(Color), 2f);
									Main.dust[num624].velocity *= 0.2f;
								}
								if (Math.Abs(Main.player[NPC.target].velocity.X) > 0.1f)
								{
									Main.player[NPC.target].velocity.X = -Main.player[NPC.target].velocity.X * 2f;
								}
								if (Math.Abs(Main.player[NPC.target].velocity.Y) > 0.1f)
								{
									Main.player[NPC.target].velocity.Y = -Main.player[NPC.target].velocity.Y * 2f;
								}
							}
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
			if (NPC.velocity.X < 0f)
			{
				NPC.spriteDirection = -1;
			}
			else if (NPC.velocity.X > 0f)
			{
				NPC.spriteDirection = 1;
			}
			bool canFly = flies;
			if (Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(false);
			}
			NPC.alpha -= 42;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 6400f || !NPC.AnyNPCs(Mod.Find<ModNPC>("EidolonWyrmTailHuge").Type))
			{
				NPC.active = false;
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
				float num47 = (float)(num42 - 600); //800
				if (!detectsPlayer)
				{
					num192 = num47;
					if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 400f) //500
					{
						if (NPC.velocity.X > 0f)
						{
							num191 = Main.player[NPC.target].Center.X + 500f; //600
						}
						else
						{
							num191 = Main.player[NPC.target].Center.X - 500f; //600
						}
					}
				}
			}
			if (detectsPlayer)
			{
				num188 = 10f;
				num189 = 0.175f;
				if (!Main.player[NPC.target].wet)
				{
					num188 = 20f;
					num189 = 0.25f;
				}
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
			if (!detectsPlayer)
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
				if (num191 < 0f)
				{
					NPC.spriteDirection = -1;
				}
				else if (num191 > 0f)
				{
					NPC.spriteDirection = 1;
				}
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
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return detectsPlayer;
			}
			return null;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (NPC.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
			Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
			Vector2 vector = center - Main.screenPosition;
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/EidolonWyrmHeadGlowHuge").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/EidolonWyrmHeadGlowHuge").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.LightYellow);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/EidolonWyrmHeadGlowHuge").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer3 && spawnInfo.Water && !NPC.AnyNPCs(Mod.Find<ModNPC>("EidolonWyrmHeadHuge").Type) &&
				!NPC.AnyNPCs(Mod.Find<ModNPC>("Reaper").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("EidolonWyrmHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("ColossalSquid").Type) &&
				CalamityWorldPreTrailer.downedPolterghast)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.006f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer4 && spawnInfo.Water && !NPC.AnyNPCs(Mod.Find<ModNPC>("EidolonWyrmHeadHuge").Type) &&
				!NPC.AnyNPCs(Mod.Find<ModNPC>("Reaper").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("EidolonWyrmHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("ColossalSquid").Type) &&
				CalamityWorldPreTrailer.downedPolterghast)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.012f;
			}
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Voidstone").Type, 1, 80, 101));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("EidolicWail").Type, 1));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("SoulEdge").Type, 1));
			npcLoot.Add(new CommonDrop(ItemID.Ectoplasm, 1, 21, 33));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Lumenite").Type, 1, 50, 109));
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1));
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("DepthCells").Type, 2, 2, 4));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Lumenite").Type, 2, 15, 28));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 15; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("WyrmAdult").Type, 1f);
				}
			}
		}

		public override bool CheckActive()
		{
			if (detectsPlayer && !Main.player[NPC.target].dead)
			{
				return false;
			}
			if (NPC.timeLeft <= 0 && Main.netMode != 1)
			{
				for (int k = (int)NPC.ai[0]; k > 0; k = (int)Main.npc[k].ai[0])
				{
					if (Main.npc[k].active)
					{
						Main.npc[k].active = false;
						if (Main.netMode == 2)
						{
							Main.npc[k].life = 0;
							Main.npc[k].netSkip = -1;
							NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			return true;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 1200, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 1200, true);
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 1200);
			}
		}
	}
}