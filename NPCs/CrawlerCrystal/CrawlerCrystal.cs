using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.CrawlerCrystal
{
	public class CrawlerCrystal : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Crystal Crawler");
			//Tooltip.SetDefault("Crystal Crawler");
			NPC.npcSlots = 0.3f;
			NPC.aiStyle = 3;
			NPC.damage = 40;
			NPC.width = 44; //324
			NPC.height = 34; //216
			NPC.defense = 18;
			NPC.lifeMax = 600;
			NPC.knockBackResist = 0.15f;
			AnimationType = 257;
			AIType = NPCID.AnomuraFungus;
			Main.npcFrameCount[NPC.type] = 5;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath36;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow,
                new FlavorTextBestiaryInfoElement("Resource pinata.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.ZoneHallow && !Main.snowMoon && !Main.pumpkinMoon ? 0.015f : 0f; 
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 68, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ItemID.CrystalShard, 1, 2, 4));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CrystalBlade>(), 30));
        }
	}
}