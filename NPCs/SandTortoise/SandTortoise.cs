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

namespace CalamityModClassic1Point1.NPCs.SandTortoise
{
	public class SandTortoise : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Sand Tortoise");
			//Tooltip.SetDefault("Sand Tortoise");
			NPC.npcSlots = 2f;
			NPC.damage = 90;
			NPC.aiStyle = 39;
			NPC.width = 46; 
			NPC.height = 32;
			NPC.defense = 34;
			NPC.scale = 1.25f;
			NPC.lifeMax = 630;
			NPC.knockBackResist = 0.2f;
			AnimationType = 153;
			Main.npcFrameCount[NPC.type] = 8;
			NPC.value = Item.buyPrice(0, 0, 25, 0);
			NPC.HitSound = SoundID.NPCHit24;
			NPC.DeathSound = SoundID.NPCDeath27;
			NPC.noGravity = false;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
                new FlavorTextBestiaryInfoElement("Taratuga.")

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
            return spawnInfo.Player.ZoneDesert && Main.hardMode && !Main.snowMoon && !Main.pumpkinMoon ? 0.04f : 0f; 
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
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 60, true);
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofCinder>(), 2));
            npcLoot.Add(new CommonDrop(ItemID.TurtleShell, 10));
        }
	}
}