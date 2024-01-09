using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.NormalNPCs
{
	public class WulfrumDrone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wulfrum Drone");
			Main.npcFrameCount[NPC.type] = 5;
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 25;
			NPC.aiStyle = 3;
			AIType = 73;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 6;
			NPC.lifeMax = 60;
			NPC.knockBackResist = 0.35f;
			NPC.value = Item.buyPrice(0, 0, 3, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("A mechanism of unknown origin.")

            });
        }

        public override void AI()
		{
			NPC.spriteDirection = ((NPC.direction > 0) ? 1 : -1);
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
			if (spawnInfo.PlayerSafe || Main.hardMode)
			{
				return 0f;
			}
			return SpawnCondition.OverworldDaySlime.Chance * 0.2f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GrassBlades, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 15; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GrassBlades, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<WulfrumShard>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<WulfrumShard>(), 2));
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
	}
}