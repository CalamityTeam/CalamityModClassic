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
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.Leviathan
{
	public class LeviathanStart : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("???");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 120; //324
			NPC.height = 120; //216
			NPC.defense = 0;
			NPC.lifeMax = 3000;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.noGravity = true;
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.rarity = 2;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Suddenly it felt like a dream.")

            });
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			NPC.TargetClosest(true);
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 0.3f);
			bool flag100 = false;
			for (int num569 = 0; num569 < 200; num569++)
			{
				if (Main.npc[num569].active && Main.npc[num569].type == (NPCID.DukeFishron))
				{
					flag100 = true;
				}
			}
			if (flag100)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || NPC.AnyNPCs(NPCID.DukeFishron) || NPC.AnyNPCs(Mod.Find<ModNPC>("LeviathanStart").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("Siren").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("Leviathan").Type))
			{
				return 0f;
			}
			if (!NPC.downedPlantBoss)
			{
				return SpawnCondition.OceanMonster.Chance * 0.035f;
			}
			return SpawnCondition.OceanMonster.Chance * 0.35f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 3000;
			NPC.damage = 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("Siren").Type);
			}
		}
	}
}