﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.BlightedEye
{
	public class BlightedEye : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Blighted Eye");
			//Tooltip.SetDefault("Blighted Eye");
			NPC.aiStyle = 2;
			NPC.damage = 52;
			NPC.width = 30; //324
			NPC.height = 32; //216
			NPC.defense = 18;
			NPC.lifeMax = 250;
			NPC.knockBackResist = 0.6f;
			AnimationType = 2;
			Main.npcFrameCount[NPC.type] = 2;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("An eyesore.")

            });
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.SpawnTileY < Main.rockLayer && Main.hardMode && !spawnInfo.PlayerInTown && !Main.dayTime && !Main.snowMoon && !Main.pumpkinMoon ? 0.05f : 0f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Weak, 100, true);
			}
			else
			{
				target.AddBuff(BuffID.Weak, 60, true);
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(new CommonDrop(ModContent.ItemType<BlightedLens>(), 2));
        }
	}
}