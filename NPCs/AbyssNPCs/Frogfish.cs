using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.AbyssNPCs
{
	public class Frogfish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frogfish");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("Is it a frog? Or is it a fish? These questions will forever confound us...")
			});
		}

		public override void SetDefaults()
		{
			NPC.chaseable = false;
			NPC.damage = 25;
			NPC.width = 60;
			NPC.height = 50;
			NPC.defense = 10;
			NPC.lifeMax = 80;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.buffImmune[Mod.Find<ModBuff>("CrushDepth").Type] = true;
			NPC.value = Item.buyPrice(0, 0, 0, 80);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("FrogfishBanner").Type;
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
					if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead)
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
						NPC.velocity.X = NPC.velocity.X + (float)NPC.direction * 0.15f;
						NPC.velocity.Y = NPC.velocity.Y + (float)NPC.directionY * 0.15f;
						if (NPC.velocity.X > 3.5f)
						{
							NPC.velocity.X = 3.5f;
						}
						if (NPC.velocity.X < -3.5f)
						{
							NPC.velocity.X = -3.5f;
						}
						if (NPC.velocity.Y > 1.5f)
						{
							NPC.velocity.Y = 1.5f;
						}
						if (NPC.velocity.Y < -1.5f)
						{
							NPC.velocity.Y = -1.5f;
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
			target.AddBuff(BuffID.Venom, 180, true);
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
			if (spawnInfo.PlayerSafe || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur)
			{
				return 0f;
			}
			return SpawnCondition.OceanMonster.Chance * 0.2f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("CloakingGland").Type, 2));
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}