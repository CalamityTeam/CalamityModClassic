using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class Cuttlefish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cuttlefish");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Despite the cuddly appearance, these squid-like creatures prove capable ambush predators.")
			});
		}

		public override void SetDefaults()
		{
			NPC.noGravity = true;
			NPC.chaseable = false;
			NPC.damage = 17;
			NPC.width = 50;
			NPC.height = 28;
			NPC.defense = 12;
			NPC.lifeMax = 55;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 1, 0);
			NPC.HitSound = SoundID.NPCHit33;
			NPC.DeathSound = SoundID.NPCDeath28;
			NPC.knockBackResist = 0.3f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CuttlefishBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<AbyssLayer1Biome>().Type, ModContent.GetInstance<AbyssLayer2Biome>().Type, ModContent.GetInstance<AbyssLayer3Biome>().Type };
		}

		public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
			int num = 200;
			if (NPC.ai[2] == 0f)
			{
				NPC.localAI[0] += 1f;
				NPC.alpha = num;
				NPC.TargetClosest(true);
				if (!Main.player[NPC.target].dead && (Main.player[NPC.target].Center - NPC.Center).Length() < 170f &&
					Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					NPC.ai[2] = -16f;
				}
				if (NPC.velocity.X != 0f || NPC.velocity.Y < 0f || NPC.velocity.Y > 2f || NPC.justHit || NPC.localAI[0] >= 420f)
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
			if (NPC.ai[2] == 1f)
			{
				NPC.chaseable = true;
				if (NPC.direction == 0)
				{
					NPC.TargetClosest(true);
				}
				if (NPC.wet || NPC.noTileCollide)
				{
					bool flag14 = false;
					NPC.TargetClosest(false);
					if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead)
					{
						flag14 = true;
					}
					if (!flag14)
					{
						if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
						{
							NPC.noTileCollide = false;
						}
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
						if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							if (NPC.ai[3] > 0f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
							{
								NPC.ai[3] = 0f;
								NPC.ai[1] = 0f;
								NPC.netUpdate = true;
							}
						}
						else if (NPC.ai[3] == 0f)
						{
							NPC.ai[1] += 1f;
						}
						if (NPC.ai[1] >= 150f)
						{
							NPC.ai[3] = 1f;
							NPC.ai[1] = 0f;
							NPC.netUpdate = true;
						}
						if (NPC.ai[3] == 0f)
						{
							NPC.alpha = 0;
							NPC.noTileCollide = false;
						}
						else
						{
							NPC.alpha = 200;
							NPC.noTileCollide = true;
						}
						NPC.TargetClosest(true);
						NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.2f;
						NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.2f;
						if (NPC.velocity.X > 9f)
						{
							NPC.velocity.X = 9f;
						}
						if (NPC.velocity.X < -9f)
						{
							NPC.velocity.X = -9f;
						}
						if (NPC.velocity.Y > 7f)
						{
							NPC.velocity.Y = 7f;
						}
						if (NPC.velocity.Y < -7f)
						{
							NPC.velocity.Y = -7f;
						}
					}
					else
					{
						if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
						{
							NPC.noTileCollide = false;
						}
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
					NPC.velocity.Y = NPC.velocity.Y + 0.25f;
					if (NPC.velocity.Y > 7f)
					{
						NPC.velocity.Y = 7f;
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

		public override void FindFrame(int frameHeight)
		{
			if (!NPC.wet && !NPC.noTileCollide)
			{
				NPC.frameCounter = 0.0;
				return;
			}
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
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
			vector -= new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/CuttlefishGlow").Value.Width, (float)(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/CuttlefishGlow").Value.Height / Main.npcFrameCount[NPC.type])) * 1f / 2f;
			vector += vector11 * 1f + new Vector2(0f, 0f + 4f + NPC.gfxOffY);
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Microsoft.Xna.Framework.Color.Gold);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AbyssNPCs/CuttlefishGlow").Value, vector,
				new Microsoft.Xna.Framework.Rectangle?(NPC.frame), color, NPC.rotation, vector11, 1f, spriteEffects, 0f);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Darkness, 120, true);
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60, true);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer1 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.3f;
			}
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyssLayer2 && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), Mod.Find<ModItem>("HalibutCannon").Type, 1000000));
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