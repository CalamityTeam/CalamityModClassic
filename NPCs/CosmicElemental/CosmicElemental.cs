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
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.CosmicElemental
{
	public class CosmicElemental : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Cosmic Elemental");
			//Tooltip.SetDefault("Cosmic Elemental");
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = 91;
			NPC.damage = 35;
			NPC.width = 20; //324
			NPC.height = 30; //216
			NPC.defense = 20;
			NPC.lifeMax = 70;
			NPC.knockBackResist = 0.5f;
			AnimationType = 483;
			Main.npcFrameCount[NPC.type] = 22;
			NPC.value = Item.buyPrice(0, 0, 9, 0);
			NPC.HitSound = SoundID.NPCHit7;
			NPC.DeathSound = SoundID.NPCDeath6;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("Swords.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            int tile = (int)Main.tile[x, y].TileType;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.8f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && Main.SceneMetrics.EvilTileCount < 80 && Main.SceneMetrics.SandTileCount < 80 && Main.SceneMetrics.JungleTileCount < 80 && Main.SceneMetrics.DungeonTileCount < 80 ? 0.0075f : 0f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Confused, 200, true);
			}
			else
			{
				target.AddBuff(BuffID.Confused, 60, true);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 70, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 70, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(new CommonDrop(ItemID.BoneSword, 10));
            npcLoot.Add(new CommonDrop(ItemID.Starfish, 50));
            npcLoot.Add(new CommonDrop(ItemID.EnchantedSword, 100));
            npcLoot.Add(new CommonDrop(ItemID.Arkhalis, 1000)); // this technically should be Terragrim 
        }
	}
}