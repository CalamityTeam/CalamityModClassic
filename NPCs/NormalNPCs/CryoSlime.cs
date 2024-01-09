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
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.NormalNPCs
{
	public class CryoSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cryo Slime");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 60;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 12;
			NPC.lifeMax = 400;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.value = Item.buyPrice(0, 0, 6, 0);
			NPC.color = new Color(0, 0, 150, 50);
			NPC.alpha = 50;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("A slime filled with crystals.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !NPC.downedMechBossAny)
			{
				return 0f;
			}
			return SpawnCondition.Cavern.Chance * 0.08f;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("MSparkle").Type, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("MSparkle").Type, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<CryonicOre>(), 1, 10, 26));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
	}
}