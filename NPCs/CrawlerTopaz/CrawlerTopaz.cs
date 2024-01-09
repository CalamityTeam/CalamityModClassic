using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.CrawlerTopaz
{
	public class CrawlerTopaz : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Topaz Crawler");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0.3f;
			NPC.aiStyle = 3;
			NPC.damage = 19;
			NPC.width = 44; //324
			NPC.height = 34; //216
			NPC.defense = 6;
			NPC.lifeMax = 50;
			NPC.knockBackResist = 0.85f;
			AnimationType = 257;
			AIType = NPCID.AnomuraFungus;
			NPC.value = Item.buyPrice(0, 0, 0, 10);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath36;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                new FlavorTextBestiaryInfoElement("A bug covered in crystals.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe)
			{
				return 0f;
			}
			return SpawnCondition.Underground.Chance * 0.0425f;
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Honey, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Honey, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ItemID.Topaz, 1, 2, 4));
        }
    }
}