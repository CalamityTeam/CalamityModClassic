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

namespace CalamityModClassic1Point2.NPCs.HiveMind
{
	public class HiveCyst : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hive Cyst");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 0f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 30; //324
			NPC.height = 30; //216
			NPC.defense = 0;
			NPC.lifeMax = 2000;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.chaseable = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.rarity = 2;
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
			if (spawnInfo.PlayerSafe || !NPC.downedBoss2 || NPC.AnyNPCs(Mod.Find<ModNPC>("HiveCyst").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("HiveMind").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("HiveMindP2").Type))
			{
				return 0f;
			}
			return SpawnCondition.Corruption.Chance * (Main.hardMode ? 0.05f : 0.2f);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 2000;
			NPC.damage = 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Demonite, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("HiveMind").Type);
			}
		}
	}
}