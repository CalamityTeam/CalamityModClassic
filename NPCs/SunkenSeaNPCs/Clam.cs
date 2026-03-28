using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassicPreTrailer.BiomeManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.SunkenSeaNPCs
{
	public class Clam : ModNPC
	{
		public int hitAmount = 0;
		public bool hasBeenHit = false;
		public bool statChange = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Clam");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				new FlavorTextBestiaryInfoElement("Regular mollusks that behave like any other.")
			});
		}

		public override void SetDefaults()
		{
			//npc.damage = Main.hardMode ? 60 : 30;
			NPC.damage = 30;
			NPC.width = 56;
			NPC.height = 38;
			//npc.defense = Main.hardMode ? 15 : 6;
			NPC.defense = 9999;
			NPC.lifeMax = Main.hardMode ? 300 : 150;
			if (Main.expertMode)
			{
				NPC.lifeMax *= 2;
			}
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.value = Main.hardMode ? Item.buyPrice(0, 0, 10, 0) : Item.buyPrice(0, 0, 1, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.knockBackResist = 0.05f;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ClamBanner").Type;
			SpawnModBiomes = new int[] { ModContent.GetInstance<SunkenSea>().Type };
		}

		public override void AI()
		{
			NPC.TargetClosest(true);
			if (Main.player[NPC.target].GetModPlayer<CalamityPlayerPreTrailer>().clamity)
			{
				hitAmount = 3;
				hasBeenHit = true;
			}
			if (NPC.justHit && hitAmount < 3)
			{
				++hitAmount;
				hasBeenHit = true;
			}
			NPC.chaseable = hasBeenHit;
			if (hitAmount == 3)
			{
				if (!statChange)
				{
					NPC.defense = 6;
					NPC.damage = Main.expertMode ? 60 : 30;
					if (Main.hardMode)
					{
						NPC.defense = 15;
						NPC.damage = Main.expertMode ? 120 : 60;
					}
					statChange = true;
				}
				if (NPC.ai[0] == 0f)
				{
					if (Main.netMode != 1)
					{
						if (NPC.velocity.X != 0f || NPC.velocity.Y < 0f || (double)NPC.velocity.Y > 0.9)
						{
							NPC.ai[0] = 1f;
							NPC.netUpdate = true;
							return;
						}
						NPC.ai[0] = 1f;
						NPC.netUpdate = true;
						return;
					}
				}
				else if (NPC.velocity.Y == 0f)
				{
					NPC.ai[2] += 1f;
					int num321 = 20;
					if (NPC.ai[1] == 0f)
					{
						num321 = 12;
					}
					if (NPC.ai[2] < (float)num321)
					{
						NPC.velocity.X = NPC.velocity.X * 0.9f;
						return;
					}
					NPC.ai[2] = 0f;
					NPC.TargetClosest(true);
					if (NPC.direction == 0)
					{
						NPC.direction = -1;
					}
					NPC.spriteDirection = -NPC.direction;
					NPC.ai[1] += 1f;
					NPC.ai[3] += 1f;
					if (NPC.ai[3] >= 4f)
					{
						NPC.ai[3] = 0f;
						if (NPC.ai[1] == 2f)
						{
							float multiplierX = (float)Main.rand.Next(3, 7);
							NPC.velocity.X = (float)NPC.direction * multiplierX;
							NPC.velocity.Y = -8f;
							NPC.ai[1] = 0f;
						}
						else
						{
							float multiplierX = (float)Main.rand.Next(5, 9);
							NPC.velocity.X = (float)NPC.direction * multiplierX;
							NPC.velocity.Y = -4f;
						}
					}
					NPC.netUpdate = true;
					return;
				}
				else
				{
					if (NPC.direction == 1 && NPC.velocity.X < 1f)
					{
						NPC.velocity.X = NPC.velocity.X + 0.1f;
						return;
					}
					if (NPC.direction == -1 && NPC.velocity.X > -1f)
					{
						NPC.velocity.X = NPC.velocity.X - 0.1f;
						return;
					}
				}
			}
			else
			{
				NPC.damage = 0;
			}
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			if (projectile.minion)
			{
				return hasBeenHit;
			}
			return null;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 4.0)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y = NPC.frame.Y + frameHeight;
			}
			if (hitAmount < 3)
			{
				NPC.frame.Y = frameHeight * 4;
			}
			else
			{
				if (NPC.frame.Y > frameHeight * 3)
				{
					NPC.frame.Y = 0;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea && spawnInfo.Water)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.2f;
			}
			return 0f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 37, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 37, hit.HitDirection, -1f, 0, default(Color), 1f);
				}

				if (Main.netMode != NetmodeID.Server)
				{
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Clam1").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Clam2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("Clam3").Type, 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Navystone").Type, 1, 8, 13));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), Mod.Find<ModItem>("MolluskHusk").Type, 2));
		}
	}
}