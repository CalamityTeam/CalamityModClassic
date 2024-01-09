﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.ModLoader.Utilities;
using CalamityModClassic1Point2.Items.Providence;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.ScornEater
{
	public class ScornEater : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Scorn Eater");
			Main.npcFrameCount[NPC.type] = 7;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 3f;
			NPC.aiStyle = -1;
			NPC.damage = 140;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.defense = 38;
			NPC.lifeMax = 23000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.lavaImmune = true;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			NPC.HitSound = SoundID.NPCHit23;
			NPC.DeathSound = SoundID.NPCDeath26;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("Pray.")

            });
        }
        public override void AI()
		{
			if (NPC.ai[0] == 0f) 
			{
				NPC.TargetClosest(true);
				if (Main.netMode != NetmodeID.MultiplayerClient) 
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
				NPC.spriteDirection = NPC.direction;
				NPC.ai[1] += 1f;
				if (NPC.ai[1] == 2f) 
				{
					NPC.velocity.X = (float)NPC.direction * 15f;
					NPC.velocity.Y = -12f;
					NPC.ai[1] = 0f;
				} 
				else 
				{
					NPC.velocity.X = (float)NPC.direction * 21f;
					NPC.velocity.Y = -6f;
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
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !NPC.downedMoonlord)
			{
				return 0f;
			}
			if (SpawnCondition.Underworld.Chance > 0f)
			{
				return SpawnCondition.Underworld.Chance / 4f;
			}
			return SpawnCondition.OverworldHallow.Chance / 4f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 2, 4));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EnergyStaff>(), 10));
        }
	}
}