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
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.StormlionCharger
{
	public class StormlionCharger : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Stormlion");
			//Tooltip.SetDefault("Stormlion");
			NPC.damage = 25;
			NPC.aiStyle = 3;
			NPC.width = 33; //324
			NPC.height = 31; //216
			NPC.defense = 18;
			NPC.lifeMax = 130;
			NPC.knockBackResist = 0.2f;
			AnimationType = 508;
			Main.npcFrameCount[NPC.type] = 6;
			NPC.value = Item.buyPrice(0, 0, 9, 0);
			NPC.HitSound = SoundID.NPCHit31;
			NPC.DeathSound = SoundID.NPCDeath34;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
                new FlavorTextBestiaryInfoElement("Don't get too close, or you'll get zapped!")

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
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.ZoneUndergroundDesert && !Main.snowMoon && !Main.pumpkinMoon ? 0.1f : 0f; 
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Electrified, 60, true);
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<StormlionMandible>(), 2));
        }
	}
}